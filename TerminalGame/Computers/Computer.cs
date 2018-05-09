﻿using Microsoft.Xna.Framework;
using System;
using TerminalGame.Computers.FileSystems;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TerminalGame.Computers
{
    class Computer
    {
        public enum Type { Workstation, Server, Mainframe, Laptop }
        Type type;
        public enum AccessLevel { root, user }
        AccessLevel access;

        public string IP { get; private set; }
        public string Name { get; private set; }
        public string RootPassword { get; private set; }
        public bool IsPlayerConnected { get; private set; }
        public bool PlayerHasRoot { get; private set; }
        public FileSystem FileSystem { get; private set; }


        public event EventHandler<ConnectEventArgs> Connected;
        public event EventHandler<ConnectEventArgs> Disonnected;

        public Computer(Type type, string IP, string Name, string RootPassword, FileSystem FileSystem)
        {
            Console.WriteLine("Create: Computer with IP " + IP + " and Name: " + Name);
            this.type = type;
            this.IP = IP;
            this.Name = Name;
            this.RootPassword = RootPassword;
            this.FileSystem = FileSystem;
        }

        public Computer(Type type, string IP, string Name, string RootPassword)
        {
            Console.WriteLine("Create: Computer with IP " + IP + " and Name: " + Name);
            this.type = type;
            this.IP = IP;
            this.Name = Name;
            this.RootPassword = RootPassword;
            BuildBasicFileSystem();
        }

        /// <summary>
        /// Sets the player's current connection to this computer
        /// </summary>
        /// <param name="GoingHome">Don't disconnect before connecting. Only used during game start, as there is no computer to disconnect from</param>
        public void Connect(bool GoingHome = false)
        {
            if (GoingHome)
            {
                Console.WriteLine("*** CONN: GOING HOME");
                Player.GetInstance().ConnectedComputer = this;
                IsPlayerConnected = true;
                //return "Connected to " + IP;
            }
            else
            {
                Player.GetInstance().ConnectedComputer.Disconnect(true);
                Player.GetInstance().ConnectedComputer = this;
                IsPlayerConnected = true;
            }
            Console.WriteLine("CONN: Calling Connected?.Invoke with IP:" + IP + " and PHR: " + PlayerHasRoot.ToString());
            Connected?.Invoke(null, new ConnectEventArgs(IP, PlayerHasRoot));
            Console.WriteLine("CONN: Connected to " + IP);
        }

        /// <summary>
        /// Disconnects the player from this computer.
        /// </summary>
        /// <param name="reconnect">Only set to true when called from Connect.</param>
        public void Disconnect(bool reconnect = false)
        {
            IsPlayerConnected = false;

            Console.WriteLine("DISC: Calling Disconnected?.Invoke");
            Disonnected?.Invoke(null, new ConnectEventArgs(IP, PlayerHasRoot));
            Console.WriteLine("DISC: Disconnecting from " + IP);
            if (reconnect)
            {
                Console.WriteLine("DISC: RECONNECT");
            }
            else
            {
                Console.WriteLine("*** DISC: GOING HOME");
                Player.GetInstance().PlayersComputer.Connect(true);
            }
        }

        private void BuildBasicFileSystem()
        {
            FileSystem = new FileSystem();
            string[] baseDirs = { "bin", "usr", "home", "sys" };
            for (int i = 0; i < baseDirs.Length; i++)
            {
                FileSystem.AddDir(baseDirs[i]);
            }
        }

        /// <summary>
        /// Grant the player eleveted (root) permission on this computer.
        /// </summary>
        public void GetRoot()
        {
            PlayerHasRoot = true;
        }

        public void Update(GameTime gameTime)
        {
            access = PlayerHasRoot ? AccessLevel.root : AccessLevel.user;
        }
    }
}
