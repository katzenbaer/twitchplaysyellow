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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Resources;
using System.Drawing.Drawing2D;
using System.Threading;

namespace TwitchPlays
{
    public partial class PokemonParty : Form
    {
        class HeadClient : WebClient
        {
            public bool HeadOnly { get; set; }
            protected override WebRequest GetWebRequest(Uri address)
            {
                WebRequest req = base.GetWebRequest(address);
                if (HeadOnly && req.Method == "GET")
                {
                    req.Method = "HEAD";
                }
                return req;
            }
        }

        private List<PictureBox> slots = new List<PictureBox>();

        private static string[] _pokemon = { "Bulbasaur", "Ivysaur", "Venusaur", "Charmander", "Charmeleon", "Charizard", "Squirtle", "Wartortle", "Blastoise", "Caterpie", "Metapod", "Butterfree", "Weedle", "Kakuna", "Beedrill", "Pidgey", "Pidgeotto", "Pidgeot", "Rattata", "Raticate", "Spearow", "Fearow", "Ekans", "Arbok", "Pikachu", "Raichu", "Sandshrew", "Sandslash", "Nidoran_f", "Nidorina", "Nidoqueen", "Nidoran_m", "Nidorino", "Nidoking", "Clefairy", "Clefable", "Vulpix", "Ninetales", "Jigglypuff", "Wigglytuff", "Zubat", "Golbat", "Oddish", "Gloom", "Vileplume", "Paras", "Parasect", "Venonat", "Venomoth", "Diglett", "Dugtrio", "Meowth", "Persian", "Psyduck", "Golduck", "Mankey", "Primeape", "Growlithe", "Arcanine", "Poliwag", "Poliwhirl", "Poliwrath", "Abra", "Kadabra", "Alakazam", "Machop", "Machoke", "Machamp", "Bellsprout", "Weepinbell", "Victreebel", "Tentacool", "Tentacruel", "Geodude", "Graveler", "Golem", "Ponyta", "Rapidash", "Slowpoke", "Slowbro", "Magnemite", "Magneton", "Farfetchd", "Doduo", "Dodrio", "Seel", "Dewgong", "Grimer", "Muk", "Shellder", "Cloyster", "Gastly", "Haunter", "Gengar", "Onix", "Drowzee", "Hypno", "Krabby", "Kingler", "Voltorb", "Electrode", "Exeggcute", "Exeggutor", "Cubone", "Marowak", "Hitmonlee", "Hitmonchan", "Lickitung", "Koffing", "Weezing", "Rhyhorn", "Rhydon", "Chansey", "Tangela", "Kangaskhan", "Horsea", "Seadra", "Goldeen", "Seaking", "Staryu", "Starmie", "Mr._Mime", "Scyther", "Jynx", "Electabuzz", "Magmar", "Pinsir", "Tauros", "Magikarp", "Gyarados", "Lapras", "Ditto", "Eevee", "Vaporeon", "Jolteon", "Flareon", "Porygon", "Omanyte", "Omastar", "Kabuto", "Kabutops", "Aerodactyl", "Snorlax", "Articuno", "Zapdos", "Moltres", "Dratini", "Dragonair", "Dragonite", "Mewtwo", "Mew", "Chikorita", "Bayleef", "Meganium", "Cyndaquil", "Quilava", "Typhlosion", "Totodile", "Croconaw", "Feraligatr", "Sentret", "Furret", "Hoothoot", "Noctowl", "Ledyba", "Ledian", "Spinarak", "Ariados", "Crobat", "Chinchou", "Lanturn", "Pichu", "Cleffa", "Igglybuff", "Togepi", "Togetic", "Natu", "Xatu", "Mareep", "Flaaffy", "Ampharos", "Bellossom", "Marill", "Azumarill", "Sudowoodo", "Politoed", "Hoppip", "Skiploom", "Jumpluff", "Aipom", "Sunkern", "Sunflora", "Yanma", "Wooper", "Quagsire", "Espeon", "Umbreon", "Murkrow", "Slowking", "Misdreavus", "Unown", "Wobbuffet", "Girafarig", "Pineco", "Forretress", "Dunsparce", "Gligar", "Steelix", "Snubbull", "Granbull", "Qwilfish", "Scizor", "Shuckle", "Heracross", "Sneasel", "Teddiursa", "Ursaring", "Slugma", "Magcargo", "Swinub", "Piloswine", "Corsola", "Remoraid", "Octillery", "Delibird", "Mantine", "Skarmory", "Houndour", "Houndoom", "Kingdra", "Phanpy", "Donphan", "Porygon2", "Stantler", "Smeargle", "Tyrogue", "Hitmontop", "Smoochum", "Elekid", "Magby", "Miltank", "Blissey", "Raikou", "Entei", "Suicune", "Larvitar", "Pupitar", "Tyranitar", "Lugia", "Ho-oh", "Celebi" };
        private List<Image>[] _images = new List<Image>[6];

        public PokemonParty()
        {
            InitializeComponent();
        }

        private string _defaultSaveDirectory;
        public PokemonParty(string defaultSaveDirectory)
        {
            InitializeComponent();
            if (defaultSaveDirectory != "") _defaultSaveDirectory = defaultSaveDirectory;
        }

        private void PokemonParty_Load(object sender, EventArgs e)
        {
            if (_defaultSaveDirectory != null)
            {
                txtSavPath.Text = _defaultSaveDirectory;
            }
            else txtSavPath.Text = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);

            slots.Add(pbSlotOne);
            slots.Add(pbSlotTwo);
            slots.Add(pbSlotThree);
            slots.Add(pbSlotFour);
            slots.Add(pbSlotFive);
            slots.Add(pbSlotSix);

            for (int i = 0; i < this._images.Count(); i++) _images[i] = new List<Image>();
        }

        private object _parse_lock = new object();
        private bool ParseSave()
        {
            if (Monitor.TryEnter(_parse_lock))
            {
                try
                {
                    foreach (PictureBox slot in slots)
                    {
                        // Stop existing animations
                        AnimatedSprite sprite = (slot.Tag is AnimatedSprite) ? (AnimatedSprite)slot.Tag : null;
                        if (sprite == null) continue;
                        else
                        {
                            sprite.StopAnimation();
                            slot.Invalidate();
                        }
                    }

                    PokeSave save = new PokeSave(txtSavPath.Text + @"\Pokemon Yellow.sav");
                    try
                    {
                        List<Pokemon> pokes = save.Team;

                        try
                        {
                            foreach (Pokemon p in pokes)
                            {
                                if (p.DexNumber == 0) continue;
                                Console.WriteLine("Found Pkmn #" + p.DexNumber + "!");

                                //string sprite_name = p.DexNumber.ToString().PadLeft(3, '0') + ".png";
                                string local_resource = @"poke_sprites/" + _pokemon[p.DexNumber - 1].ToLower() + ".gif";
                                if (!Directory.Exists(@"poke_sprites"))
                                {
                                    Console.WriteLine("PokemonParty sprites directory does not exist. Creating one...");
                                    Directory.CreateDirectory(@"poke_sprites");
                                }
                                if (!File.Exists(local_resource))
                                {
                                    /*using (WebClient Client = new WebClient())
                                    {
                                        Client.DownloadFile(@"http://www.serebii.net/pokedex-xy/icon/" + sprite_name, sprite_file);
                                    }*/
                                    string remote_url = @"http://www.pkparaiso.com/imagenes/xy/sprites/animados/";
                                    using (HeadClient Client = new HeadClient())
                                    {
                                        string remote_resource_name = _pokemon[p.DexNumber - 1].ToLower() + ".gif";
                                        Client.DownloadFile(remote_url + remote_resource_name, @"poke_sprites/" + remote_resource_name);
                                    }
                                    using (HeadClient Client = new HeadClient())
                                    {
                                        for (int i = 2; i <= 5; i++)
                                        {
                                            string remote_resource_name = _pokemon[p.DexNumber - 1].ToLower() + "-" + i.ToString() + ".gif";
                                            Client.HeadOnly = true;
                                            try
                                            {
                                                Client.DownloadData(remote_url + remote_resource_name);
                                                Client.HeadOnly = false;
                                                Client.DownloadFile(remote_url + remote_resource_name, @"poke_sprites/" + remote_resource_name);
                                            }
                                            catch
                                            {
                                                Console.WriteLine("Couldn't find remote resource: " + remote_resource_name);
                                                break;
                                            }
                                        }
                                    }
                                    Console.WriteLine("Done downloading available sprites.");
                                }
                            }
                        }
                        catch
                        {
                            Console.WriteLine("Unable to download pkmn team sprites");
                        }

                        try
                        {
                            for (int i = 0; i < pokes.Count; i++)
                            {

                                //string sprite_name = pokes[i].DexNumber.ToString().PadLeft(3, '0') + ".png";
                                string sprite_name = _pokemon[pokes[i].DexNumber - 1].ToLower() + ".gif";
                                string sprite_file = @"poke_sprites/" + sprite_name;
                                //slots[i].ImageLocation = sprite_file;

                                if (!File.Exists(sprite_file))
                                {
                                    Console.WriteLine("Sprite file does not exist: " + sprite_file);
                                    continue;
                                }

                                Image image = null;
                                try
                                {
                                    image = Image.FromFile(sprite_file);
                                }
                                catch (Exception e)
                                {
                                    Program.ExitWithError(e, "Error loading sprite image.");
                                }

                                _images[i].Clear();
                                _images[i].Add(image);
                                for (int alt = 2; alt <= 5; alt++)
                                {
                                    string local_resource_name = @"poke_sprites/" + _pokemon[pokes[i].DexNumber - 1].ToLower() + "-" + alt.ToString() + ".gif";
                                    if (File.Exists(local_resource_name))
                                    {
                                        _images[i].Add(Image.FromFile(local_resource_name));
                                    }
                                    else break;
                                }

                                //if (_images[i].Width == 192 && _images[i].Height == 192) {
                                {
                                    // Let Paint draw it.
                                    slots[i].Image = null;
                                    try
                                    {
                                        slots[i].Tag = new AnimatedSprite(slots[i], _images[i]);
                                    }
                                    catch
                                    {
                                        slots[i].Tag = null;
                                        Console.WriteLine("Unable to create AnimatedSprite.");
                                    };
                                }/* else {
                                slots[i].Image = _images[i];
                            }*/
                            }
                        }
                        catch
                        {
                            Console.WriteLine("Unable to set pkmn team sprites");
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Unable to extract team.");
                    }
                    Thread.Sleep(1000 * 5); // Hold so duplicate calls fall through.
                    return true;
                }
                catch
                {
                    Console.WriteLine("PokemonParty: Save file does not exist.");
                }
                finally
                {
                    Monitor.Exit(_parse_lock);
                }
            }
            else Console.WriteLine("Couldn't get parse file lock");

            return false;
        }

        private void btnCheckSave_Click(object sender, EventArgs e)
        {
            Thread parse_thread = new Thread(() => ParseSave());
            parse_thread.IsBackground = true;
            parse_thread.Start();
        }

        private void OnChanged(object source, FileSystemEventArgs e)
        {
            Thread parse_thread = new Thread(() => ParseSave());
            parse_thread.IsBackground = true;
            parse_thread.Start();
        }

        private void StartWatch()
        {
            FileSystemWatcher fsw = new FileSystemWatcher(txtSavPath.Text);
            fsw.NotifyFilter = NotifyFilters.LastWrite;
            fsw.Filter = "Pokemon Yellow.sav";
            fsw.Changed += new FileSystemEventHandler(OnChanged);

            fsw.EnableRaisingEvents = true;
        }

        private void ParseThenWatch()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(
                            () =>
                            {
                                btnWatch.Enabled = false;
                            })
                );
            }
            else if (Thread.CurrentThread.Name == "Main") btnWatch.Enabled = false;
            if (ParseSave()) StartWatch();
        }

        private void btnWatch_Click(object sender, EventArgs e)
        {
            Thread parse_thread = new Thread(() => ParseThenWatch());
            parse_thread.IsBackground = true;
            parse_thread.Start();
        }

        private void btnBrowseDirectory_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowNewFolderButton = false;
            fbd.Description = "Description here";
            fbd.SelectedPath = txtSavPath.Text;

            if (fbd.ShowDialog() == DialogResult.OK) txtSavPath.Text = fbd.SelectedPath;
        }

        private void PokemonParty_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program._shouldExit = true;
            System.Environment.Exit(0);
        }

        private void PokemonParty_Shown(object sender, EventArgs e)
        {
            Thread parse_thread = new Thread(() => ParseThenWatch());
            parse_thread.IsBackground = true;
            parse_thread.Start();
        }
    }
}
