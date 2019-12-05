using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ADVYTEAM.Data;
using ADVYTEAM.Services;
using Microsoft.AspNet.SignalR;

namespace LiveChat.signalr.hubs
{
    public class ChatHub : Hub
    {
        IUtilisateurService serviceUtilisateur;
        public ChatHub()
        {
            serviceUtilisateur = new UtilisateurService();
        }
        public void Send(string name, string message,string id)
        {
            utilisateur user = serviceUtilisateur.GetById(Int32.Parse(id));
            string  dt = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt");
            Clients.All.addNewMessageToPage(name, message,dt, user.image);
        }

    }
}