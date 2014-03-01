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
    public class User
    {
        public enum ModeType {
            NONE,
            DEMOCRACY,
            ANARCHY
        };

        private string _name;
        private Int32 _totalVotes;
        private DateTime _lastActive;
        private Nominee _lastNominee;
        private Nominee _lastDownNominee;
        private bool _isAdmin;
        private ModeType _gameMode = ModeType.NONE;

        public User(string n)
        {
            this._name = n;
            this._totalVotes = 0;
            this._lastActive = new DateTime(2014, 01, 01);
            this._isAdmin = false;
        }

        public string Name
        {
            set
            {
                _name = value;
            }
            get
            {
                return _name;
            }
        }
        public void IncrementVotes()
        {
            _totalVotes++;
        }
        public Int32 TotalVotes
        {
            get
            {
                return _totalVotes;
            }
        }
        public DateTime LastActive
        {
            set
            {
                _lastActive = value;
            }
        }

        public bool IsActive
        {
            get
            {
                return (DateTime.Now - _lastActive).TotalMinutes <= 30;
                //return (DateTime.Now - _lastActive).TotalMinutes <= 10;
            }
        }

        public Nominee LastNominee
        {
            set
            {
                _lastNominee = value;
            }
            get
            {
                return _lastNominee;
            }
        }

        public Nominee LastDownNominee
        {
            set
            {
                _lastDownNominee = value;
            }
            get
            {
                return _lastDownNominee;
            }
        }

        public bool IsAdmin
        {
            get
            {
                return _isAdmin;
            }
            set
            {
                _isAdmin = value;
                if (_isAdmin) Console.WriteLine(Name + " is now an admin!");
            }
        }

        public ModeType GameMode
        {
            get
            {
                return _gameMode;
            }
            set
            {
                _gameMode = value;
            }
        }
    }
}
