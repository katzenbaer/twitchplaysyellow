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
    class WebsiteFilter
    {
        public enum FilterType {
            WHITELIST,
            BLACKLIST
        }
        private string _domain;
        private FilterType _type;

        public WebsiteFilter(string domain, FilterType type)
        {
            this._domain = domain;
            this._type = type;
        }

        public string Domain
        {
            get
            {
                return _domain;
            }
        }

        public FilterType Type
        {
            get {
                return _type;
            }
        }
    }
}
