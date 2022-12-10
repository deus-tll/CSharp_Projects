using System.Windows;
using System.Windows.Input;
using ChatClient.ServiceChat;

namespace ChatClient
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window, IServiceChatCallback
	{
		bool isConnected = false;
		ServiceChat.ServiceChatClient client;
		int ID;

		public MainWindow()
		{
			InitializeComponent();
		}

		private void ConnectUser()
		{
			if (!isConnected)
			{
				client = new ServiceChatClient(new System.ServiceModel.InstanceContext(this));
				ID = client.Connect(tbUserName.Text);

				tbUserName.IsEnabled= false;
				btnConDiscon.Content = "Disconnect";
				isConnected = true;
			}
		}

		private void DisconnectUser()
		{
			if (isConnected)
			{
				client.Disconnect(ID);
				client = null;

				tbUserName.IsEnabled = true;
				btnConDiscon.Content = "Connect";
				isConnected = true;
			}
		}

		private void btnConDiscon_Click(object sender, RoutedEventArgs e)
		{
			if (isConnected)
			{
				DisconnectUser();
			}
			else
			{
				ConnectUser();
			}
		}

		public void MsgCallBack(string msg)
		{
			lbChat.Items.Add(msg);
			lbChat.ScrollIntoView(lbChat.Items[lbChat.Items.Count - 1]);
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			DisconnectUser();
		}

		private void tbMessage_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				if (client != null)
				{
					client.SendMsg(tbMessage.Text, ID);
					tbMessage.Text = string.Empty;
				}
			}
		}
	}
}
