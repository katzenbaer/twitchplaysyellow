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
    public class Nominee
    {
        private int _votes;
        private List<Program.KeyType> _keys = null;
        private List<uint> _counts = null;

        public Nominee(Program.KeyType k)
        {
            this._votes = 0;

            _keys = new List<Program.KeyType>();
            _counts = new List<uint>();
            this._keys.Add(k);
            this._counts.Add(1);
        }
        public Nominee(Program.KeyType k, uint cnt)
        {
            this._votes = 0;

            _keys = new List<Program.KeyType>();
            _counts = new List<uint>();
            this._keys.Add(k);
            this._counts.Add(cnt);
        }
        public Nominee(List<Program.KeyType> ks, List<uint> cnts)
        {
            this._votes = 0;
            this._keys = new List<Program.KeyType>(ks);
            this._counts = new List<uint>(cnts);
        }

        public string Name
        {
            get
            {
                if (this._counts.Count == 1 && this._counts[0] == 1)
                    return Program.KeyMap.First(x => x.Value == this._keys[0]).Key;
                else
                {
                    string _name = "";
                    for (int i = 0; i < this._keys.Count; i++)
                    {
                        _name += Program.KeyMap.First(x => x.Value == this._keys[i]).Key;
                        _name += this._counts[i].ToString();
                    }
                    return _name;
                }
            }
        }

        public int Votes
        {
            get
            {
                return _votes;
            }
        }

        public void Clear()
        {
            _votes = 0;
        }

        public void Vote()
        {
            _votes++;
        }

        public void Unvote()
        {
            _votes--;
        }

        public List<Program.KeyType> Keys
        {
            get
            {
                return _keys;
            }
        }

        public List<uint> Counts
        {
            get
            {
                return _counts;
            }
        }

        public long CountsSum
        {
            get
            {
                return _counts.Sum(x => x);
            }
        }

        public int Length
        {
            get
            {
                return _keys.Count;
            }
        }
    }
}
