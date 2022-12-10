using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace wcf_chat
{

	[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
	public class ServiceChat : IServiceChat
	{
		private List<ServerUser> users = new List<ServerUser>();
		private int nextId = 1;

		public int Connect(string name)
		{
			ServerUser user = new ServerUser()
			{
				ID = nextId,
				Name = name,
				operationContext = OperationContext.Current
			};
			nextId++;

			SendMsg(": " + user.Name + " підключився до чату.", 0);
			users.Add(user);
			return user.ID;
		}

		public void Disconnect(int id)
		{
			var user = users.FirstOrDefault(i => i.ID == id);
			if (user != null) 
			{
				users.Remove(user);
				SendMsg(": " + user.Name + " покинув чат.", 0);
			}
		}
		  
		public void SendMsg(string msg, int id)
		{
			foreach (var user in users)
			{
				string answer = DateTime.Now.ToShortTimeString();

				var _user = users.FirstOrDefault(i => i.ID == id);
				if (_user != null)
				{
					answer += ": " + _user.Name + " ";
				}

				answer += msg;

				user.operationContext.GetCallbackChannel<IServerChatCallBack>().MsgCallBack(answer);
			}
		}
	}
}
