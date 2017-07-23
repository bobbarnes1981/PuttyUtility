using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace PuttyUtility
{
    public partial class FormMain : Form
    {
        private const string CONFIG_PATH = "puttyutility.config";

        private const string TAG_SESSION = "SESSION";

        private const string TAG_FOLDER = "FOLDER";

        private Controller m_controller;

        private ConfigurationManager m_configurationManager;

        private Configuration m_configuration;

        public FormMain()
        {
            InitializeComponent();

            m_controller = new Controller(new RegistryAccessor());

            m_configurationManager = new ConfigurationManager(CONFIG_PATH);

            m_configuration = m_configurationManager.Load();

            treeViewSessions.Nodes.Add(new TreeNode { Text = "Sessions", Tag = TAG_FOLDER });
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            loadTreeFromConfiguration(treeViewSessions.Nodes[0], m_configuration.Sessions);

            loadSessionsFromRegistry();

            loadMenuFromTree(sessionsToolStripMenuItem, treeViewSessions.Nodes[0]);
        }

        /// <summary>
        /// Load session information from configuration file and populate the tree view with it,
        /// creates folder and session nodes where required, does not load sessions that are not
        /// in the registry.
        /// </summary>
        /// <param name="parentTreeNode"></param>
        /// <param name="parentConfigNode"></param>
        private void loadTreeFromConfiguration(TreeNode parentTreeNode, ConfigNode parentConfigNode)
        {
            foreach (ConfigNode node in parentConfigNode.Nodes)
            {
                switch (node.Type)
                {
                    case ConfigNodeType.Folder:
                        TreeNode folderTreeNode = getFolderTreeNode(parentTreeNode, node.Name);
                        loadTreeFromConfiguration(folderTreeNode, node);
                        break;

                    case ConfigNodeType.Session:
                        if (getSessionFromRegistry(node.Name) != null)
                        {
                            parentTreeNode.Nodes.Add(new TreeNode {Text = node.Name, Tag = TAG_SESSION});
                        }
                        break;

                    default:
                        throw new Exception(string.Format("Invalid node type: {0}", node.Type));
                }
            }
        }

        /// <summary>
        /// Gets a tree node with tag TAG_FOLDER which is a direct child node of parentTreeNode,
        /// creates node if it does not exist
        /// </summary>
        /// <param name="parentTreeNode"></param>
        /// <param name="folderName"></param>
        /// <returns></returns>
        private TreeNode getFolderTreeNode(TreeNode parentTreeNode, string folderName)
        {
            foreach (TreeNode node in parentTreeNode.Nodes)
            {
                if (node.Tag.ToString() == TAG_FOLDER && node.Text == folderName)
                {
                    return node;
                }
            }

            return addFolderTreeNode(parentTreeNode, folderName);
        }

        private TreeNode addFolderTreeNode(TreeNode parentTreeNode, string folderName)
        {
            TreeNode n = new TreeNode { Text = folderName, Tag = TAG_FOLDER };
            parentTreeNode.Nodes.Add(n);
            return n;
        }

        /// <summary>
        /// Gets a tree node with tag TAG_SESSION which exists anywhere beneath the parentTreeNode,
        /// returns null if it does not exist
        /// </summary>
        /// <param name="parentTreeNode"></param>
        /// <param name="sessionName"></param>
        /// <returns></returns>
        private TreeNode getSessionTreeNode(TreeNode parentTreeNode, string sessionName)
        {
            foreach (TreeNode node in parentTreeNode.Nodes)
            {
                switch (node.Tag.ToString())
                {
                    case TAG_SESSION:
                        if (node.Text == sessionName)
                        {
                            return node;
                        }
                        break;

                    case TAG_FOLDER:
                        TreeNode n = getSessionTreeNode(node, sessionName);
                        if (n != null)
                        {
                            return n;
                        }
                        break;

                    default:
                        throw new Exception(string.Format("Invalid tree node tag: {0}", node.Tag.ToString()));
                }
            }

            return null;
        }


        private Session getSessionFromRegistry(string sessionName)
        {
            List<Session> sessions = m_controller.GetSessions();

            foreach (Session session in sessions)
            {
                if (session.Name == sessionName)
                {
                    return session;
                }
            }

            return null;
        }

        /// <summary>
        /// Load session information from the registry and insert any sessions not already in the tree
        /// </summary>
        private void loadSessionsFromRegistry()
        {
            List<Session> sessions = m_controller.GetSessions();

            foreach (Session session in sessions)
            {
                TreeNode node = getSessionTreeNode(treeViewSessions.Nodes[0], session.Name);
                if (node == null)
                {
                    treeViewSessions.Nodes[0].Nodes.Add(new TreeNode { Text = session.Name, Tag = TAG_SESSION });
                }
            }
        }

        /// <summary>
        /// Load session information from tree view and populate the context menu with it,
        /// creates folder and session nodes where required
        /// </summary>
        private void loadMenuFromTree(ToolStripMenuItem parentMenuItem, TreeNode parentTreeNode)
        {
            foreach (TreeNode node in parentTreeNode.Nodes)
            {
                switch (node.Tag.ToString())
                {
                    case TAG_FOLDER:
                        ToolStripMenuItem folderMenuItem = getFolderMenuItem(parentMenuItem, node.Text);
                        loadMenuFromTree(folderMenuItem, node);
                        break;

                    case TAG_SESSION:
                        ToolStripMenuItem sessionToolStripMenuItem = new ToolStripMenuItem { Text = node.Text, Tag = TAG_SESSION };
                        sessionToolStripMenuItem.Click += (object sender, EventArgs e) =>
                        {
                            Process p = new Process();
                            p.StartInfo.FileName = m_configuration.PuttyPath;
                            p.StartInfo.Arguments = string.Format("-load \"{0}\"", node.Text);
                            p.Start();
                        };

                        parentMenuItem.DropDownItems.Add(sessionToolStripMenuItem);
                        break;

                    default:
                        throw new Exception(string.Format("Invalid node type: {0}", node.Tag));
                }
            }
        }

        /// <summary>
        /// Load session information from tree view and populate the configuration with it,
        /// creates folder and session nodes where required
        /// </summary>
        private void loadConfigurationFromTree(ConfigNode configNode, TreeNode parentTreeNode)
        {
            foreach (TreeNode node in parentTreeNode.Nodes)
            {
                switch (node.Tag.ToString())
                {
                    case TAG_FOLDER:
                        ConfigNode f = getFolderConfigNode(configNode, node.Text);
                        loadConfigurationFromTree(f, node);
                        break;

                    case TAG_SESSION:
                        ConfigNode s = new ConfigNode { Name = node.Text, Type = ConfigNodeType.Session };
                        configNode.Nodes.Add(s);
                        break;

                    default:
                        throw new Exception(string.Format("Invalid node type: {0}", node.Tag));
                }
            }
        }

        /// <summary>
        /// Gets a menu item with tag TAG_FOLDER which is a direct child node of parentMenuItem,
        /// creates item if it does not exist
        /// </summary>
        /// <param name="parentMenuItem"></param>
        /// <param name="folderName"></param>
        /// <returns></returns>
        private ToolStripMenuItem getFolderMenuItem(ToolStripMenuItem parentMenuItem, string folderName)
        {
            foreach (ToolStripMenuItem item in parentMenuItem.DropDownItems)
            {
                if (item.Tag.ToString() == TAG_FOLDER && item.Text == folderName)
                {
                    return item;
                }
            }

            ToolStripMenuItem sessionToolStripMenuItem = new ToolStripMenuItem { Text = folderName, Tag = TAG_FOLDER };
            parentMenuItem.DropDownItems.Add(sessionToolStripMenuItem);
            return sessionToolStripMenuItem;
        }

        /// <summary>
        /// Gets a config node with FOLDER type which is a direct child node of configNode,
        /// creates item if it does not exist
        /// </summary>
        /// <param name="parentMenuItem"></param>
        /// <param name="folderName"></param>
        /// <returns></returns>
        private ConfigNode getFolderConfigNode(ConfigNode configNode, string folderName)
        {
            foreach (ConfigNode node in configNode.Nodes)
            {
                if (node.Type == ConfigNodeType.Folder && node.Name == folderName)
                {
                    return node;
                }
            }

            ConfigNode n = new ConfigNode { Name = folderName, Type = ConfigNodeType.Folder };
            configNode.Nodes.Add(n);
            return n;
        }

        private void toolStripMenuItemExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void deleteFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode folder = treeViewSessions.SelectedNode;

            if (MessageBox.Show(this, string.Format("Delete the folder {0}?", folder.Text), "Delete Folder", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                // TODO: what to do with child folders and sessions
            }
        }

        private void newFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormNewFolder folder = new FormNewFolder();

            if (folder.ShowDialog() == DialogResult.OK)
            {
                TreeNode parentTreeNode = treeViewSessions.SelectedNode;

                addFolderTreeNode(parentTreeNode, folder.FolderName);

                loadConfigurationFromTree(m_configuration.Sessions, treeViewSessions.Nodes[0]);

                loadMenuFromTree(sessionsToolStripMenuItem, treeViewSessions.Nodes[0]);

                m_configurationManager.Save(m_configuration);
            }
        }

        private void contextMenuStripTreeView_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            TreeNode node = treeViewSessions.SelectedNode;

            switch (node.Tag.ToString())
            {
                case TAG_FOLDER:
                    deleteFolderToolStripMenuItem.Enabled = true;
                    newFolderToolStripMenuItem.Enabled = true;
                    break;

                case TAG_SESSION:
                    deleteFolderToolStripMenuItem.Enabled = false;
                    newFolderToolStripMenuItem.Enabled = false;
                    break;

                default:
                    throw new Exception(string.Format("Invalid node type: {0}", node.Tag));
            }
        }

        private void treeViewSessions_ItemDrag(object sender, ItemDragEventArgs e)
        {

        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormOptions options = new FormOptions(m_configuration);

            if (options.ShowDialog() == DialogResult.OK)
            {
                m_configuration = options.Configuration;

                m_configurationManager.Save(m_configuration);
            }
        }
    }
}
