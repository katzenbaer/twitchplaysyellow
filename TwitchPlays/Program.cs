/**
 * This file is part of TwitchPlays.
 * 
 * Copyright (C) 2014 Ash. Katzenbaer
 * All Rights Reserved.
 * 
 * @github im420blaziken
 *  
 * TwitchPlays is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * TwitchPlays is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with TwitchPlays.  If not, see <http://www.gnu.org/licenses/>.
 */
using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Meebey.SmartIrc4net;

namespace TwitchPlays
{
    public static class Program
    {
        static class ModFunctions
        {
            public static bool SetGoal(User u, string msg) {
                /*if (u.IsAdmin)
                {
                    _logform.SetAdminGoal(u, msg);
                    return true;
                }*/ // Disable mod permission to bypass set goal restriction
                return _logform.SetGoal(u, msg); 
            }

            public static bool ClearGoal(User u) {
                string msg = "[EXPLETIVE DELETED]";
                if (u.IsAdmin)
                {
                    _logform.ClearAdminGoal(u, msg);
                    return true;
                }
                lock (Users.ClearGoal)
                {
                    if (!Users.ClearGoal.Contains(u)) Users.ClearGoal.Add(u);
                }
                return _logform.ClearGoal(new User("Majority vote"), msg); 
            }

            public static void SetLiveMode(User u, bool live)
            {
                if (u.IsAdmin)
                {
                    if (live)
                    {
                        GameMode = User.ModeType.ANARCHY;
                        SendIRCNotice("Twippy wants anarchy!");
                    }
                    else {
                        GameMode = User.ModeType.DEMOCRACY;
                        SendIRCNotice("Twippy wants TwitchPlays.");
                    }  
                }
            }

            public static bool ReloadFilters(User u)
            {
                if (u.IsAdmin)
                {
                    try
                    {
                        // Website Blacklist
                        string blacklistPath = @"blacklist.txt";
                        if (!File.Exists(blacklistPath)) using (File.Create(blacklistPath)) { };

                        using (StreamReader sr = new StreamReader(blacklistPath))
                        {
                            string s = null;
                            while (null != (s = sr.ReadLine())) WebsiteFilters.Add(new WebsiteFilter(s, WebsiteFilter.FilterType.BLACKLIST));
                        }

                        // Website Blacklist
                        string whitelistPath = @"whitelist.txt";
                        if (!File.Exists(whitelistPath)) using (File.Create(whitelistPath)) { };
                        using (StreamReader sr = new StreamReader(whitelistPath))
                        {
                            string s = null;
                            while (null != (s = sr.ReadLine())) WebsiteFilters.Add(new WebsiteFilter(s, WebsiteFilter.FilterType.WHITELIST));
                        }
                    }
                    catch (System.IO.IOException e)
                    {
                        ExitWithError(e, "blacklist/whitelist");
                        return false;
                    }
                }
                return true;
            }

            private static DateTime _lastGoal = DateTime.Now;
            public static bool GetGoal(User u)
            {
                if (u.IsAdmin)
                {
                    SendIRCNotice("The goal is: " + _logform.Goal);
                    return true;
                }
                else
                {
                    if ((DateTime.Now - _lastGoal).TotalMinutes >= 1)
                    {
                        SendIRCNotice("The goal is: " + _logform.Goal);
                        _lastGoal = DateTime.Now;
                        return true;
                    }
                }
                return false;
            }

            public static bool LoadState(User u)
            {
                if (u.IsAdmin)
                {
                    // TODO
                    //SendIRCNotice("The goal is: " + _logform.Goal);
                    return true;
                }
                return false;
            }
            public static bool SaveState(User u)
            {
                if (u.IsAdmin)
                {
                    // TODO
                    //SendIRCNotice("The goal is: " + _logform.Goal);
                    return true;
                }
                return false;
            }

            public static bool ReloadGame(User u) {
                if (u.IsAdmin)
                {
                    _logform.ReloadGame();
                    return true;
                }
                return false;
            }

            public static bool SaveGame(User u)
            {
                if (u.IsAdmin)
                {
                    _logform.SaveGame();
                    return true;
                }
                return false;
            }

            public static bool ClearQuietMode(User u)
            {
                if (u.IsAdmin)
                {
                    Program.ProductionMode = true;
                    return true;
                }
                return false;
            }
            public static bool SetQuietMode(User u)
            {
                if (u.IsAdmin)
                {
                    Program.ProductionMode = false;
                    return true;
                }
                return false;
            }

            private static DateTime _executionTime = DateTime.Now;
            public static DateTime ExecutionTime
            {
                set
                {
                    _executionTime = value;
                }
            }
            public static bool GetUptime(User u)
            {
                if (u.IsAdmin)
                {
                    SendIRCNotice("Last restart was " + (DateTime.Now - _executionTime).ToString(@"d'd 'h'h 'm'm 's's'") + " ago.");
                    return true;
                }
                return false;
            }

            public static bool BlameGoal(User u)
            {
                if (u.IsAdmin)
                {
                    SendIRCNotice(_logform.GoalSetter + " last touched the goal.");
                    return true;
                }
                return false;
            }

            private static void april_fools()
            {
                lock (Users.All)
                {
                    if (GameMode == User.ModeType.ANARCHY) GameMode = User.ModeType.DEMOCRACY;
                    else if (GameMode == User.ModeType.DEMOCRACY) GameMode = User.ModeType.ANARCHY;

                    foreach (User u in Users.All)
                    {
                        if (u.GameMode == User.ModeType.ANARCHY) u.GameMode = User.ModeType.DEMOCRACY;
                        else if (u.GameMode == User.ModeType.DEMOCRACY) u.GameMode = User.ModeType.ANARCHY;
                    }
                }
            }
            public static bool ClearAprilFools(User u)
            {
                if (u.IsAdmin && Program.IsAprilFools)
                {
                    Program.IsAprilFools = false;
                    april_fools();
                    return true;
                }
                return false;
            }
            public static bool SetAprilFools(User u)
            {
                if (u.IsAdmin && !Program.IsAprilFools)
                {
                    Program.IsAprilFools = true;
                    april_fools();
                    return true;
                }
                return false;
            }
        }

        public static IrcClient irc = new IrcClient();
        public static LogForm _logform = null;

        public enum KeyType
        {
            START = 0,
            SELECT,
            A,
            B,
            LEFT,
            RIGHT,
            UP,
            DOWN,
            HOLD,
            NONE,
        };
        private static Dictionary<string, KeyType> keyMap = new Dictionary<string, KeyType>();
        private static List<Nominee> nominees = new List<Nominee>();

#if DEBUG
        private static bool ProductionMode = false;
        
#else
        private static bool ProductionMode = true;
#endif
        private static string username = null;
        private static string password = null;
        private static string channel 
        {
            get
            {
#if STRESS_TEST
                return "#twitchplayspokemon";
#else
                return "#" + username;
#endif
            }
        }
#if EVENT_SERVER
        private static string server = "199.9.252.26";
#else
        private static string server = "irc.twitch.tv";
#endif
        private static int port = 6667;

        private static bool _aprilfools = false;
        public static bool IsAprilFools
        {
            get
            {
                return _aprilfools;
            }
            set
            {
                _aprilfools = value;
            }
        }

        public static Dictionary<string, KeyType> KeyMap
        {
            get
            {
                return keyMap;
            }
        }
        private static DateTime _runStart = new DateTime(2014, 2, 16, 19, 0, 0).ToLocalTime();
        public static DateTime RunStart
        {
            get
            {
                return _runStart;
            }
        }

        private static DateTime _tallyTime = DateTime.Now;
        public static DateTime TallyTime
        {
            get
            {
                return _tallyTime;
            }
            set
            {
                _tallyTime = value;
            }
        }

        private static DateTime _lastInteraction = DateTime.Now;
        public static DateTime LastInteraction
        {
            get
            {
                return _lastInteraction;
            }
            set
            {
                _lastInteraction = value;
            }
        }

        public static List<Nominee> SortNominees()
        {
            lock (nominees)
            {
                nominees.Sort(delegate(Nominee x, Nominee y)
                {
                    return y.Votes.CompareTo(x.Votes);
                });
                return Nominees;
            }
        }
        public static List<Nominee> ValidNominees()
        {
            return nominees.FindAll(x => x.Votes > 0);
        }
        public static void ClearNominees()
        {
            lock (Users.Voted)
            {
                Users.Voted.Clear();
            }
            lock (Users.Downvoted)
            {
                Users.Downvoted.Clear();
            }
            lock (nominees)
            {
                nominees.Clear();
            }
        }

        public static List<Nominee> Nominees
        {
            get
            {
                return new List<Nominee>(nominees);
            }
        }

        private static double _maxdelay = 8.0;
        public static double MaxDelay
        {
            get
            {
                return _maxdelay;
            }
            set
            {
                _maxdelay = value;
            }
        }
        private static double _voteDuration = 0;
        public static double VoteDuration
        {
            get
            {
                return _voteDuration;
            }
            set
            {
                _voteDuration = Math.Round(2.0 * (MaxDelay * (1.0 - Math.Pow(2.0, -1.0 * ((double)value - 18.0) / 1024.0))), MidpointRounding.AwayFromZero) / 2.0;
            }
        }

        private static User.ModeType _gameMode = User.ModeType.NONE;
        public static User.ModeType GameMode
        {
            get
            {
                return _gameMode;
            } 
            set
            {
                _gameMode = value;
                if (_logform != null) _logform.OnGameMode(_gameMode);
            }
        }

        private static double _durationOffset = 30.0;
        public static double DurationOffset
        {
            get
            {
                return _durationOffset;
            }
            set
            {
                _durationOffset = value;
            }
        }

        //private static double _lagPropagationDelay = 15.0;
        private static double _lagPropagationDelay = 0.0;
        public static double LagPropagationDelay
        {
            get
            {
                return _lagPropagationDelay;
            }
        }

        public static TimeSpan VoteSpan
        {
            get
            {
                return Program.TallyTime - DateTime.Now;
            }
        }

        public static bool IsVotingClosed
        {
            get
            {
                //return (Program.VoteDuration + Program.DurationOffset - Program.LagPropagationDelay) <= VoteSpan.TotalSeconds;
                return false;
            }
        }

        private static bool _shouldAutosave = true;
        public static bool ShouldAutosave
        {
            get
            {
                return _shouldAutosave;
            }
            set
            {
                _shouldAutosave = value;
            }
        }

        // this method handles when we receive "ERROR" from the IRC server
        public static void OnError(object sender, Meebey.SmartIrc4net.ErrorEventArgs e)
        {
            ExitWithMsg("OnError: " + e.ErrorMessage);
        }

        public static void ShowMessage(string user, string msg)
        {
            string left_msg = "";

            if (GameMode == User.ModeType.DEMOCRACY) left_msg = "[#" + _logform.VoteCount.ToString().PadLeft(3, '0') + "] " + user;
            else left_msg = "[" + DateTime.Now.Minute.ToString().PadLeft(2, '0') + ":" + DateTime.Now.Second.ToString().PadLeft(2, '0') + "] " + user;

            msg = " " + msg;
            if (msg.Length > 33) msg = msg.Substring(0, 33);
            if (left_msg.Length + msg.Length > 33) left_msg = left_msg.Substring(0, 33 - msg.Length);
            _logform.SetLastMsg(left_msg + msg.PadLeft(33 - left_msg.Length, ' '));
        }

        private static void BanUser(User u, bool timeout, string reason)
        {
            if (!u.IsAdmin)
            {
                if (timeout)
                {
                    SendIRCMessage(".timeout " + u.Name);
                    Thread.Sleep(1000);
                    SendIRCMessage("/timeout " + u.Name);
                    Log("[BAN] Put " + u.Name + " in timeout for " + reason);
                }
                else
                {
                    SendIRCMessage(".ban " + u.Name);
                    Thread.Sleep(1000);
                    SendIRCMessage("/ban " + u.Name);
                    Log("[BAN] Banned " + u.Name + " for " + reason);
                }
            }
            else Log("[BAN] Cannot ban admin or mod " + u.Name + " for " + reason);
        }

        static List<WebsiteFilter> _websiteFilters = new List<WebsiteFilter>();
        private static List<WebsiteFilter> WebsiteFilters
        {
            get
            {
                return _websiteFilters;
            }
        }
        public static void OnChannelMessage(object sender, IrcEventArgs e)
        {
            // Active Flags
            User u = null;
            lock (Users.All)
            {
                u = Users.All.Find(x => x.Name.Equals(e.Data.Nick.ToLower()));
            }
            if (u == null)
            {
                u = CreateUser(e.Data.Nick, false);
            }

            u.LastActive = DateTime.Now;
            u.IncrementVotes();
            LastInteraction = DateTime.Now;

            Log(e.Data.Nick + ": " + e.Data.Message);
            if (!_logform.InputEnabled) return; // Disable Voting UI Flag

            // Website Filters
            string[] explicit_blacklist = { "dotcom", "(dot)", "░", "█" };
            foreach (string m in explicit_blacklist)
            {
                if (e.Data.Message.ToLower().Contains(m))
                {
                    BanUser(u, false, "filter evasion / Unicode art");
                    return;
                }
            }

            Regex filter = new Regex(@"[A-z0-9-]+(?:\.[A-z]{2,3})+", RegexOptions.RightToLeft);
            string stripped_message = Regex.Replace(e.Data.Message, @"[()]", "");
            bool is_clean = true;
            List<string> dirty_matches = new List<string>();
            foreach (Match m in filter.Matches(stripped_message))
            {
                WebsiteFilter website_filter = WebsiteFilters.Find(x => x.Domain.Equals(m.Value));
                if (website_filter != null)
                {
                    if (website_filter.Type == WebsiteFilter.FilterType.BLACKLIST) BanUser(u, false, "blacklisted website: '" + m.Value + "'");
                    return;
                }
                else
                {
                    is_clean = false;
                    dirty_matches.Add(m.Value);
                }
            }
            if (!is_clean)
            {
                BanUser(u, true, "unlisted website(s): " + String.Join(", ", dirty_matches));
                return;
            }

            Nominee keyNominee = null;
            string command = e.Data.Message.ToUpper();
            bool downvote = false;
            if (command.StartsWith("NO") || command.StartsWith("-"))
            {
                downvote = true;
                if (command.StartsWith("NOT")) command = command.Substring(3);
                else if (command.StartsWith("NO")) command = command.Substring(2);
                else if (command.StartsWith("-")) command = command.Substring(1);
                command.TrimStart(' ');
            }
            if (command.StartsWith("@")) // Function Command
            {
                string[] function_msg = command.Substring(1).Split(' ');
                if (function_msg.Count() == 0) return;
                string opcode = function_msg.First();
                string[] payload = function_msg.Skip(1).ToArray();

                Console.WriteLine("Function! Opcode: " + opcode + ", Payload: " + String.Join(" ", payload));

                switch (opcode)
                {
                    case "BLAME":
                        if (payload.Count() == 0) return;
                        switch (payload.First())
                        {
                            case "GOAL": ModFunctions.BlameGoal(u); break;
                        }
                        break;
                    case "GET":
                        if (payload.Count() == 0) return;
                        switch (payload.First())
                        {
                            case "GOAL": ModFunctions.GetGoal(u); break;
                            case "UPTIME": ModFunctions.GetUptime(u); break;
                        }
                        break;
                    case "SET":
                        if (payload.Count() == 0) return;
                        switch (payload.First())
                        {
                            case "GOAL": ModFunctions.SetGoal(u, String.Join(" ", payload.Skip(1))); break;
                            case "QUIETMODE": if (ModFunctions.SetQuietMode(u)) SendIRCNotice("Quiet mode on."); break;
                            case "R9K": if (ModFunctions.SetAprilFools(u)) SendIRCNotice("r9k mode on."); break;
                        }
                        break;
                    case "CLEAR":
                        if (payload.Count() == 0) return;
                        switch (payload.First())
                        {
                            case "GOAL": if (!ModFunctions.ClearGoal(u)) Log("Unable to get goal, last get goal was less than 1 minute ago."); break;
                            case "QUIETMODE": if (ModFunctions.ClearQuietMode(u)) SendIRCNotice("Quiet mode off."); break;
                            case "R9K": if (ModFunctions.ClearAprilFools(u)) SendIRCNotice("r9k mode off."); break;
                        }
                        break;
                    case "ENABLE":
                        if (payload.Count() == 0) return;
                        switch (payload.First())
                        {
                            case "INPUT": break; // TODO
                        }
                        break;
                    case "DISABLE":
                        if (payload.Count() == 0) return;
                        switch (payload.First())
                        {
                            case "INPUT": break; // TODO
                        }
                        break;
                    case "SAVE":
                        if (payload.Count() == 0) return;
                        switch (payload.First())
                        {
                            case "STATE":
                                if (ModFunctions.SaveState(u)) SendIRCNotice("Saved the twitchplays state.");
                                break;
                            case "GAME":
                                if (ModFunctions.SaveGame(u)) SendIRCNotice("The game has been saved.");
                                break;
                        }
                        break;
                    case "RELOAD":
                        if (payload.Count() == 0) return;
                        switch (payload.First())
                        {
                            case "FILTERS":
                                if (ModFunctions.ReloadFilters(u)) SendIRCNotice("Reloaded filter lists...");
                                break;
                            case "STATE":
                                if (ModFunctions.LoadState(u)) SendIRCNotice("Reloaded the twitchplays state.");
                                break;
                            case "GAME":
                                if (ModFunctions.ReloadGame(u)) SendIRCNotice("The game has been reloaded.");
                                break;
                        }
                        break;
                    case "MUTE": // TODO
                        break;
                    default:
                        break;
                }
            }
            else if (command.Equals("ANARCHY"))
            {
                if (IsAprilFools)
                {
                    ShowMessage(e.Data.Nick, (u.GameMode == User.ModeType.DEMOCRACY) ? "*DEMOCRACY" : "DEMOCRACY");
                    u.GameMode = User.ModeType.DEMOCRACY;
                }
                else
                {
                    ShowMessage(e.Data.Nick, (u.GameMode == User.ModeType.ANARCHY) ? "*ANARCHY" : "ANARCHY");
                    u.GameMode = User.ModeType.ANARCHY;
                }
            }
            else if (command.Equals("DEMOCRACY"))
            {
                if (IsAprilFools)
                {
                    ShowMessage(e.Data.Nick, (u.GameMode == User.ModeType.ANARCHY) ? "*ANARCHY" : "ANARCHY");
                    u.GameMode = User.ModeType.ANARCHY;
                }
                else
                {
                    ShowMessage(e.Data.Nick, (u.GameMode == User.ModeType.DEMOCRACY) ? "*DEMOCRACY" : "DEMOCRACY");
                    u.GameMode = User.ModeType.DEMOCRACY;
                }
            } else {
                if (e.Data.Message.Length > 35) return; // don't parse long messages
                // Check to make sure it starts with one of our keys.
                foreach (string key in KeyMap.Keys)
                {
                    if (command.StartsWith(key)) goto _goto_valid_command;
                }
                return; // not valid command

            _goto_valid_command:
                Regex regex = new Regex(@"(?:(?<key>[a-z]+)\s*(?<count>\d?\d?))\s*", RegexOptions.IgnoreCase);

                List<KeyType> keys = new List<KeyType>();
                List<uint> counts = new List<uint>();
                foreach (Match match in regex.Matches(command)) {
                    GroupCollection groups = match.Groups;

                    string _key = groups["key"].Value.ToUpper();
                    if (keyMap.ContainsKey(_key))
                    {
                        uint count = 0;
                        UInt32.TryParse(groups["count"].Value, out count);

                        if (count == 0 || (_key.Equals("START") && count > 1)) count = 1;

                        keys.Add(keyMap[_key]);
                        counts.Add(count);
                    }
                    else break;
                }
                if (keys.Count > 0)
                {
                    keyNominee = new Nominee(keys, counts); // Load if already exists
                    if (keyNominee.CountsSum > 20) // No more than 20 queued inputs
                    {
                        ShowMessage(e.Data.Nick, "#" + keyNominee.Name);
                        return;
                    }

                    Nominee existing = null;
                    if (null != (existing = nominees.Find(x => x.Name.Equals(keyNominee.Name)))) keyNominee = existing;
                }
            }

            if (keyNominee != null)
            {
                if (GameMode == User.ModeType.ANARCHY) // Live Mode
                {
                    Nominee n = new Nominee(keyNominee.Keys[0], 1);
                    _logform.vJoySendNominee(n);

                    u.LastNominee = n;
                    ShowMessage(e.Data.Nick, n.Name);
                    return;
                }
                else if (GameMode == User.ModeType.DEMOCRACY)
                {
                    if (!nominees.Contains(keyNominee)) nominees.Add(keyNominee);

                    string right_msg = (downvote) ? "-" + keyNominee.Name : keyNominee.Name;

                    if (IsVotingClosed)
                    {
                        right_msg = "#" + right_msg;
                    }
                    else
                    {
                        bool contains = false;
                        if (downvote) {
                            lock (Users.Downvoted)
                            {
                                contains = Users.Downvoted.Contains(u);
                            }
                        } else {
                            lock (Users.Voted)
                            {
                                contains = Users.Voted.Contains(u);
                            }
                        }
                        if (contains)
                        {
                            Nominee lastNominee = (downvote) ? u.LastDownNominee : u.LastNominee;

                            if (lastNominee == keyNominee)
                            {
                                right_msg = "*" + right_msg;
                            }
                            else
                            {
                                if (lastNominee != null) if (downvote) lastNominee.Vote(); else lastNominee.Unvote();
                                if (downvote) keyNominee.Unvote(); else keyNominee.Vote();

                                right_msg = ((downvote) ? "-" + lastNominee.Name : lastNominee.Name) + "=>" + right_msg;
                            }
                        }
                        else
                        {
                            if (downvote)
                            {
                                lock (Users.Downvoted)
                                {
                                    Users.Downvoted.Add(u);
                                }
                                keyNominee.Unvote();
                            }
                            else
                            {
                                lock (Users.Voted)
                                {
                                    Users.Voted.Add(u);
                                }
                                keyNominee.Vote();
                            }
                        }

                        if (downvote) u.LastDownNominee = keyNominee;
                        else u.LastNominee = keyNominee;
                    }

                    ShowMessage(e.Data.Nick, right_msg);
                }
                else
                {
                    // Unable to find key, wtf?
                }
            }
        }

        public static bool IsMainThread
        {
            get
            {
                return Thread.CurrentThread.Name == "Main";
            }
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        private enum Argument
        {
            Server,
            Username,
            Password,
            DefaultSavDirectory,
            DefaultMode,
            Autosave
        };
        [STAThread]
        static void Main(string[] args)
        {
            Thread.CurrentThread.Name = "Main";
            string defaultSaveDirectory = "";

            ModFunctions.ExecutionTime = DateTime.Now;

            // Command-line arguments
            // <server> username password <default sav directory> <default mode> <autosave=1>
            for (uint i = 0; i < args.Count(); i++)
            {
                Argument argi = (Argument)i;
                string argv = args[i];
                switch (argi)
                {
                    case Argument.Server:
                        {
                            if (argv.Length != 0) server = argv;
                        }
                        break;
                    case Argument.Username:
                        {
                            if (argv.Length != 0) username = argv;
                        }
                        break;
                    case Argument.Password:
                        {
                            if (argv.Length != 0) password = argv;
                        }
                        break;
                    case Argument.DefaultSavDirectory:
                        {
                            if (argv.Length != 0)
                            {
                                defaultSaveDirectory = argv;
                                Console.WriteLine("Default Save Directory: " + defaultSaveDirectory);
                            }
                        }
                        break;
                    case Argument.DefaultMode:
                        {
                            if (argv.Length != 0)
                            {
                                if (argv.Equals("ANARCHY")) GameMode = User.ModeType.ANARCHY;
                                else if (argv.Equals("DEMOCRACY")) GameMode = User.ModeType.DEMOCRACY;
                                Console.WriteLine("Default Mode: " + args[1]);
                            }
                        }
                        break;
                    case Argument.Autosave:
                        {
                            if (argv.Length != 0)
                            {
                                if (argv.Equals("1")) ShouldAutosave = true;
                                else if (argv.Equals("0")) ShouldAutosave = false;
                                Console.WriteLine("Autosave: " + args[1]);
                            }
                        }
                        break;
                }
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LogForm(defaultSaveDirectory));
        }

        public static void SendIRCNotice(string msg)
        {
            SendIRCMessage(".me : " + msg);
        }

        public static void SendIRCMessage(string msg)
        {
            if (ProductionMode && _logform.InputEnabled)
            {
                try
                {
                    if (irc != null) irc.SendMessage(SendType.Message, channel, msg);
                }
                catch (Exception e)
                {
                    ExitWithError(e, "Unable to send IRC message.");
                }
            }
            Log(msg);
        }

        private static void OnConnected(object sender, EventArgs args)
        {
            Console.WriteLine("OnConnected handler called.");
            // Login
            try
            {
                Users.Clear(); // Reset our user lists
                Console.WriteLine("--Connected");

                irc.Login(username, "realname", 0, username, password);

                if (ProductionMode) SendIRCMessage("/r9kbetaoff");

                Log("Connected to IRC. Joining channel: " + channel);
                irc.RfcJoin(channel);

                irc.Listen();

                try
                {
                    irc.Disconnect(); // when our IRC session is over
                }
                catch (NotConnectedException e)
                {
                    Console.WriteLine("Couldn't disconnect when not connected: " + e.Message);
                }
                ExitWithMsg("Disconnected from server.");
            }
            catch (ConnectionException e)
            {
                ExitWithError(e, "OnConnected (Specified)");
            }
            catch (Exception e)
            {
                ExitWithError(e, "OnConnected (Unspecified)");
            }
        }

        public static User CreateUser(string name, bool admin)
        {
            lock (Users.All)
            {
                User u = Users.All.Find(x => x.Name.Equals(name.ToLower()));
                if (u == null)
                {
                    u = new User(name.ToLower());
                    u.IsAdmin = admin;
                    Users.All.Add(u);
                    return u;
                }
                u.IsAdmin = admin;
                return u;
            }
        }

        private static void OnJoin(object sender, JoinEventArgs e)
        {
            if (!irc.IsMe(e.Who))
            {
                //CreateUser(e.Who, false);
                Log(e.Who + " joined.");
                Console.WriteLine(e.Who + " has joined!");
            }
            else
            {
                CreateUser(e.Who, true); // TwitchTV will never recognize the streamer as an admin if this isn't here.
            }
            Users.TotalUsers++;
            /*_playercount++;
            VoteDuration = _playercount;
            _logform.SetUserCount((int)_playercount);*/
            //VoteDuration = Users.All.Count;
            //_logform.SetUserCount(Users.All.Count);
        }
        private static void OnPart(object sender, PartEventArgs e)
        {
            if (irc.IsMe(e.Who)) Console.WriteLine("We left!");
            else
            {
                Log(e.Who + " parted.");
                /*if (Users.RemoveAll(x => x.Name.Equals(e.Who)) > 0)
                {
                    Console.WriteLine(e.Who + " has left!");
                    //VoteDuration = Users.All.Count;
                    //_logform.SetUserCount(Users.All.Count);
                }
                else Console.WriteLine(e.Who + " has left, but we were unable to remove them.");*/
            }
            Users.TotalUsers--;
        }

        private static void OnOp(object sender, OpEventArgs e) {
            User u = null;
            lock (Users.All)
            {
                u = Users.All.Find(x => x.Name.Equals(e.Whom.ToLower()));
            }
            if (u != null) u.IsAdmin = true;
            else CreateUser(e.Whom, true);
        }
        private static void OnDeop(object sender, DeopEventArgs e) {
            User u = null;
            lock (Users.All)
            {
                u = Users.All.Find(x => x.Name.Equals(e.Whom.ToLower()));
            }
            if (u != null) u.IsAdmin = false;
            else CreateUser(e.Whom, false);
        }
        private static void OnNames(object sender, NamesEventArgs e) {
            Users.TotalUsers += e.UserList.Count();
            /*uint user_count = 0;
            foreach (string user in new List<string>(e.UserList))
            {
                CreateUser(user, false);
                user_count++;
            }

            Console.WriteLine("We joined, and there are already " + user_count + " users!");*/
        }

        private static int _disconnectCounter = 0;
        private static int _disconnectWait = 0;
        public static bool _shouldExit = false;
        public static void OnDisconnected(object sender, EventArgs e)
        {
            if (_shouldExit == false)
            {
                _disconnectWait += (int)Math.Pow(2, _disconnectCounter - 1) - _disconnectCounter;
                Thread.Sleep(_disconnectWait * 1000);
                ConnectIRC(null);
            }
        }

        public static void ConnectIRC(LogForm _form) {
            if (_form != null) _logform = _form;

            if (Thread.CurrentThread.Name == null)
            {
                Thread.CurrentThread.Name = "IRC Thread";

                irc.Encoding = System.Text.Encoding.UTF8;
                irc.SendDelay = 200;
                irc.ActiveChannelSyncing = true;
                irc.AutoRetry = true;

                //irc.OnQueryMessage += new IrcEventHandler(OnQueryMessage);
                irc.OnError += new Meebey.SmartIrc4net.ErrorEventHandler(OnError);
                //irc.OnPong += new PongEventHandler(DetermineMode);
                irc.OnJoin += new JoinEventHandler(OnJoin);
                irc.OnPart += new PartEventHandler(OnPart);
                irc.OnOp += new OpEventHandler(OnOp);
                irc.OnDeop += new DeopEventHandler(OnDeop);
                irc.OnNames += new NamesEventHandler(OnNames);
                //irc.OnRawMessage += new IrcEventHandler(OnRawMessage);
                irc.OnChannelMessage += new IrcEventHandler(OnChannelMessage);
                irc.OnConnected += new EventHandler(OnConnected);
                irc.OnDisconnected += new EventHandler(OnDisconnected);

                //if (GameMode == User.ModeType.NONE) GameMode = User.ModeType.ANARCHY;

                // KeyMap
                keyMap.Add("HOLD", KeyType.HOLD);
                keyMap.Add("STAY", KeyType.HOLD);
                keyMap.Add("STOP", KeyType.HOLD);
                keyMap.Add("START", KeyType.START);
                keyMap.Add("SELECT", KeyType.SELECT);
                keyMap.Add("UP", KeyType.UP);
                keyMap.Add("NORTH", KeyType.UP);
                keyMap.Add("DOWN", KeyType.DOWN);
                keyMap.Add("SOUTH", KeyType.DOWN);
                keyMap.Add("LEFT", KeyType.LEFT);
                keyMap.Add("WEST", KeyType.LEFT);
                keyMap.Add("RIGHT", KeyType.RIGHT);
                keyMap.Add("EAST", KeyType.RIGHT);
                keyMap.Add("A", KeyType.A);
                keyMap.Add("B", KeyType.B);

                User u = new User("Command-line Argument");
                u.IsAdmin = true;
                ModFunctions.ReloadFilters(u);
            }

            // Connect
            if (username == null || password == null) ExitWithMsg("Username or password has not been set!");

            string[] serverlist;
            serverlist = new string[] { server };
            
            try
            {
                irc.Connect(serverlist, port);
            }
            catch (Exception e)
            {
                //ExitWithError(e, "Couldn't connect.");
            }
        }

        private static void Log(string msg)
        {
            if (_logform.InvokeRequired)
            {
                _logform.Invoke(new Action(
                    () =>
                    {
                        _logform.Log(msg);
                    }
                    )
                );
            }
            else if (Program.IsMainThread)
            {
                _logform.Log(msg);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void ExitWithError(Exception e, string msg)
        {
            string caller = new StackFrame(1, true).GetMethod().Name;
            string exceptionName = e.GetType().FullName;
            string errorMsg = "Message: " + msg + "\r\n" + exceptionName + ": " + e.StackTrace;

            Log(errorMsg);
            Console.WriteLine(errorMsg);
            ExitWithMsg(errorMsg, caller);
        }

        public static void ExitWithMsg(string msg)
        {
            ExitWithMsg(msg, new StackFrame(1, true).GetMethod().Name);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void ExitWithMsg(string msg, string caller)
        {
            try
            {
                _logform.DumpLog();
            }
            catch { }
            // we are done, lets exit...
            _shouldExit = true;
            if (MessageBox.Show("Caller: " + caller + "\r\n" + msg, "Exit", MessageBoxButtons.OK) == DialogResult.OK) System.Environment.Exit(0);
        }
    }
}
