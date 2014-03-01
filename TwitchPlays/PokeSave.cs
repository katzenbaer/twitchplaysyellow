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
using System.IO;

namespace TwitchPlays
{
    class Pokemon
    {
        private byte[] _species2dex = {
                                       000, 112, 115, 032, 035, 021, 100, 034, 080, 002, 103, 108, 102, 088, 094, 029, //  0x0 -  0xF
                                       031, 104, 111, 131, 059, 151, 130, 090, 072, 092, 123, 120, 009, 127, 114, 000, // 0x10 - 0x1F
                                       000, 058, 095, 022, 016, 079, 064, 075, 113, 067, 122, 106, 107, 024, 047, 054, // 0x20 - 0x2F
                                       096, 076, 000, 126, 000, 125, 082, 109, 000, 056, 086, 050, 128, 000, 000, 000, // 0x30 - 0x3F
                                       083, 048, 149, 000, 000, 000, 084, 060, 124, 146, 144, 145, 132, 052, 098, 000, // 0x40 - 0x4F
                                       000, 000, 037, 038, 025, 026, 000, 000, 147, 148, 140, 141, 116, 117, 000, 000, // 0x50 - 0x5F
                                       027, 028, 138, 139, 039, 040, 133, 136, 135, 134, 066, 041, 023, 046, 061, 111, // 0x60 - 0x6F
                                       013, 014, 015, 000, 085, 057, 051, 049, 087, 000, 000, 010, 011, 012, 068, 000, // 0x70 - 0x7F
                                       055, 097, 042, 150, 143, 129, 000, 000, 089, 000, 099, 091, 000, 101, 036, 110, // 0x80 - 0x8F
                                       053, 105, 000, 093, 063, 065, 017, 018, 121, 001, 003, 073, 000, 118, 119, 000, // 0x90 - 0x9F
                                       000, 000, 000, 077, 078, 019, 020, 033, 030, 074, 137, 142, 000, 081, 000, 000, // 0xA0 - 0xAF
                                       004, 007, 005, 008, 006, 000, 000, 000, 000, 043, 044, 045, 069, 070, 071, 000, // 0xB0 - 0xBE
                                      };
        private byte _species = 0;

        public Pokemon(byte species)
        {
            _species = species;
        }

        public byte Species 
        {
            get
            {
                return _species;
            }
        }
        public int DexNumber
        {
            get
            {
                if (_species != 0) return _species2dex[_species];
                else return 0;
            }
        }
    }

    class PokeSave
    {
        private MemoryStream _data;
        private List<Pokemon> _team;

        public PokeSave(string path)
        {
            if (!File.Exists(path)) throw new ArgumentException("Save file does not exist: '" + path + "'");

            try
            {
                _data = new MemoryStream();
                using (Stream input = File.OpenRead(path))
                {
                    input.CopyTo(_data);
                }
                _data.Position = 0;
            }
            catch (System.IO.IOException e)
            {
                Console.WriteLine("Unable to load save: " + e.Message);
            }
        }

        public List<Pokemon> Team
        {
            get
            {
                if (_team == null) // Fetch team data
                {
                    _team = new List<Pokemon>();
                    _data.Position = 0x2F2C;
                    int count = _data.ReadByte();
                    for (int i = 0; i < count; i++)
                    {
                        int species = _data.ReadByte();
                        _team.Add(new Pokemon((byte)species));
                    }
                    if (_data.ReadByte() != 0xFF) throw new InvalidDataException("Unable to locate expected species list terminator byte at 0x" + String.Format("{0:X}", (_data.Position - 1)));
                }
                return _team;
            }
        }
    }
}
