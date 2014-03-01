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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchPlays
{
    public static class Users
    {
        public enum UserAction
        {
            ALL,
            VOTED,
            DOWNVOTED,
            CLEARGOAL,
            MUTED
        };

        private static Dictionary<UserAction, List<User>> _users = new Dictionary<UserAction, List<User>>() {
            {UserAction.ALL, new List<User>()},
            {UserAction.VOTED, new List<User>()},
            {UserAction.DOWNVOTED, new List<User>()},
            {UserAction.CLEARGOAL, new List<User>()},
            {UserAction.MUTED, new List<User>()},
        };

        public static void Clear()
        {
            foreach (List<User> list in _users.Values) {
                lock (list)
                {
                    list.Clear();
                }
            }
            _totalUsers = 0;
        }

        public static int RemoveInactive()
        {
            int ret = 0;
            foreach (List<User> list in _users.Values)
            {
                lock (list)
                {
                    if (list == All) ret = list.RemoveAll(p => !p.IsActive && !p.IsAdmin);
                    else list.RemoveAll(p => !p.IsActive && !p.IsAdmin);
                }
            }
            return ret;
        }

        /*public static int RemoveAll(Predicate<User> p)
        {
            int ret = 0;
            foreach (List<User> list in _users.Values)
            {
                lock (list)
                {
                    if (list == All) ret = list.RemoveAll(p);
                    else list.RemoveAll(p);
                }
            }
            return ret;
        }*/

        /*public static List<User> Active
        {
            get
            {
                return All.FindAll(x => x.IsActive);
            }
        }*/

        public static List<User> All
        {
            get
            {
                return _users[UserAction.ALL];
            }
        }
        public static List<User> Voted
        {
            get
            {
                return _users[UserAction.VOTED];
            }
        }
        public static List<User> Downvoted
        {
            get
            {
                return _users[UserAction.DOWNVOTED];
            }
        }
        public static List<User> ClearGoal
        {
            get
            {
                return _users[UserAction.CLEARGOAL];
            }
        }
        public static List<User> Muted
        {
            get
            {
                return _users[UserAction.MUTED];
            }
        }

        private static int _totalUsers = 0;
        public static int TotalUsers
        {
            get
            {
                return _totalUsers;
            }
            set
            {
                _totalUsers = value;
                if (_totalUsers < 0) _totalUsers = 0;
            }
        }
    }
}
