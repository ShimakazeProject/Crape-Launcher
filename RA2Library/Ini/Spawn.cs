using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RA2Library.Ini
{
    public class Spawn
    {
        private string Path;
        private IniFile spawn;

        public Alliances Multi1_Alliances;
        public Alliances Multi2_Alliances;
        public Alliances Multi3_Alliances;
        public Alliances Multi4_Alliances;
        public Alliances Multi5_Alliances;
        public Alliances Multi6_Alliances;
        public Alliances Multi7_Alliances;
        public Alliances Multi8_Alliances;
        #region Settings
        public void Settings_AIDifficulty(uint value) =>
            spawn.AddValue("Settings", "AIDifficulty", value);
        public void Settings_AIPlayers(uint value) =>
            spawn.AddValue("Settings", "AIPlayers", value);
        public void Settings_AlliesAllowed(bool value) =>
            spawn.AddValue("Settings", "AlliesAllowed", value);
        public void Settings_AttackNeutralUnits(bool value) =>
            spawn.AddValue("Settings", "AttackNeutralUnits", value);
        public void Settings_AutoDeployMCV(bool value) =>
            spawn.AddValue("Settings", "AutoDeployMCV", value);
        public void Settings_Bases(bool value) =>
            spawn.AddValue("Settings", "Bases", value);
        public void Settings_BridgeDestroy(bool value) =>
            spawn.AddValue("Settings", "BridgeDestroy", value);
        public void Settings_BuildOffAlly(bool value) =>
            spawn.AddValue("Settings", "BuildOffAlly", value);
        public void Settings_Color(uint value) =>
            spawn.AddValue("Settings", "Color", value);
        public void Settings_Creates(bool value) =>
            spawn.AddValue("Settings", "Creates", value);
        public void Settings_Credits(uint value) =>
            spawn.AddValue("Settings", "Credits", value);
        public void Settings_CustomLoadScreen(string value) =>
            spawn.AddValue("Settings", "CustomLoadScreen", value);
        public void Settings_DifficultyModeComputer(uint value) =>
            spawn.AddValue("Settings", "DifficultyModeComputer", value);
        public void Settings_DifficultyModeHuman(uint value) =>
            spawn.AddValue("Settings", "DifficultyModeHuman", value);
        public void Settings_Firestorm(bool value) =>
            spawn.AddValue("Settings", "Firestorm", value);
        public void Settings_FogOfWar(bool value) =>
            spawn.AddValue("Settings", "FogOfWar", value);
        public void Settings_GameID(ulong value) =>
            spawn.AddValue("Settings", "GameID", value);
        public void Settings_GameMode(uint value) =>
            spawn.AddValue("Settings", "GameMode", value);
        public void Settings_GameSpeed(uint value) =>
            spawn.AddValue("Settings", "GameSpeed", value);
        public void Settings_Host(bool value) =>
            spawn.AddValue("Settings", "Host", value);
        public void Settings_IsSinglePlayer(bool value) =>
            spawn.AddValue("Settings", "IsSinglePlayer", value);
        public void Settings_IsSpectator(bool value) =>
            spawn.AddValue("Settings", "IsSpectator", value);
        public void Settings_LoadSaveGame(bool value) =>
            spawn.AddValue("Settings", "LoadSaveGame", value);
        public void Settings_MapHash(string value) =>
            spawn.AddValue("Settings", "MapHash", value);
        public void Settings_MCVRedeploy(bool value) =>
           spawn.AddValue("Settings", "MCVRedeploy", value);
        public void Settings_MultiEngineer(bool value) =>
           spawn.AddValue("Settings", "MultiEngineer", value);
        public void Settings_Name(string value) =>
           spawn.AddValue("Settings", "Name", value);
        public void Settings_PlayerCount(uint value) =>
            spawn.AddValue("Settings", "PlayerCount", value);
        public void Settings_Port(long value) =>
            spawn.AddValue("Settings", "Port", value);
        public void Settings_SaveGameName(string value) =>
           spawn.AddValue("Settings", "SaveGameName", value);
        public void Settings_Scenario(string value) =>
           spawn.AddValue("Settings", "Scenario", value);
        public void Settings_Seed(ulong value) =>
            spawn.AddValue("Settings", "Seed", value);
        public void Settings_ShortGame(bool value) =>
            spawn.AddValue("Settings", "ShortGame", value);
        public void Settings_Side(uint value) =>
            spawn.AddValue("Settings", "Side", value);
        public void Settings_SidebarHack(bool value) =>
            spawn.AddValue("Settings", "SidebarHack", value);
        public void Settings_SkipScoreScreen(bool value) =>
            spawn.AddValue("Settings", "SkipScoreScreen", value);
        public void Settings_Superweapons(bool value) =>
            spawn.AddValue("Settings", "Superweapons", value);
        public void Settings_TechLevel(uint value) =>
            spawn.AddValue("Settings", "TechLevel", value);
        public void Settings_UIGameMode(string value) =>
            spawn.AddValue("Settings", "UIGameMode", value);
        public void Settings_UIMapName(string value) =>
            spawn.AddValue("Settings", "UIMapName", value);
        public void Settings_UnitCount(uint value) =>
            spawn.AddValue("Settings", "UnitCount", value);
        #endregion
        #region SpawnLocations
        public void SpawnLocations_Multi1(ushort value) =>
            spawn.AddValue("SpawnLocations", "Multi1", value);
        public void SpawnLocations_Multi2(ushort value) =>
            spawn.AddValue("SpawnLocations", "Multi2", value);
        public void SpawnLocations_Multi3(ushort value) =>
            spawn.AddValue("SpawnLocations", "Multi3", value);
        public void SpawnLocations_Multi4(ushort value) =>
            spawn.AddValue("SpawnLocations", "Multi4", value);
        public void SpawnLocations_Multi5(ushort value) =>
            spawn.AddValue("SpawnLocations", "Multi5", value);
        public void SpawnLocations_Multi6(ushort value) =>
            spawn.AddValue("SpawnLocations", "Multi6", value);
        public void SpawnLocations_Multi7(ushort value) =>
            spawn.AddValue("SpawnLocations", "Multi7", value);
        public void SpawnLocations_Multi8(ushort value) =>
            spawn.AddValue("SpawnLocations", "Multi8", value);
        #endregion
        #region HouseCountries
        public void HouseCountries_Multi1(ushort value) =>
            spawn.AddValue("HouseCountries", "Multi1", value);
        public void HouseCountries_Multi2(ushort value) =>
            spawn.AddValue("HouseCountries", "Multi2", value);
        public void HouseCountries_Multi3(ushort value) =>
            spawn.AddValue("HouseCountries", "Multi3", value);
        public void HouseCountries_Multi4(ushort value) =>
            spawn.AddValue("HouseCountries", "Multi4", value);
        public void HouseCountries_Multi5(ushort value) =>
            spawn.AddValue("HouseCountries", "Multi5", value);
        public void HouseCountries_Multi6(ushort value) =>
            spawn.AddValue("HouseCountries", "Multi6", value);
        public void HouseCountries_Multi7(ushort value) =>
            spawn.AddValue("HouseCountries", "Multi7", value);
        public void HouseCountries_Multi8(ushort value) =>
            spawn.AddValue("HouseCountries", "Multi8", value);
        #endregion
        #region HouseColors
        public void HouseColors_Multi1(ushort value) =>
            spawn.AddValue("HouseColors", "Multi1", value);
        public void HouseColors_Multi2(ushort value) =>
            spawn.AddValue("HouseColors", "Multi2", value);
        public void HouseColors_Multi3(ushort value) =>
            spawn.AddValue("HouseColors", "Multi3", value);
        public void HouseColors_Multi4(ushort value) =>
            spawn.AddValue("HouseColors", "Multi4", value);
        public void HouseColors_Multi5(ushort value) =>
            spawn.AddValue("HouseColors", "Multi5", value);
        public void HouseColors_Multi6(ushort value) =>
            spawn.AddValue("HouseColors", "Multi6", value);
        public void HouseColors_Multi7(ushort value) =>
            spawn.AddValue("HouseColors", "Multi7", value);
        public void HouseColors_Multi8(ushort value) =>
            spawn.AddValue("HouseColors", "Multi8", value);
        #endregion
        #region HouseHandicaps
        public void HouseHandicaps_Multi1(ushort value) =>
            spawn.AddValue("HouseHandicaps", "Multi1", value);
        public void HouseHandicaps_Multi2(ushort value) =>
            spawn.AddValue("HouseHandicaps", "Multi2", value);
        public void HouseHandicaps_Multi3(ushort value) =>
            spawn.AddValue("HouseHandicaps", "Multi3", value);
        public void HouseHandicaps_Multi4(ushort value) =>
            spawn.AddValue("HouseHandicaps", "Multi4", value);
        public void HouseHandicaps_Multi5(ushort value) =>
            spawn.AddValue("HouseHandicaps", "Multi5", value);
        public void HouseHandicaps_Multi6(ushort value) =>
            spawn.AddValue("HouseHandicaps", "Multi6", value);
        public void HouseHandicaps_Multi7(ushort value) =>
            spawn.AddValue("HouseHandicaps", "Multi7", value);
        public void HouseHandicaps_Multi8(ushort value) =>
            spawn.AddValue("HouseHandicaps", "Multi8", value);
        #endregion
        #region Tunnel
        public void Tunnel_Ip(string value) =>
            spawn.AddValue("Tunnel", "Ip", value);
        public void Tunnel_Port(ushort value) =>
            spawn.AddValue("Tunnel", "Port", value);
        #endregion
        #region Other
        public void Other_Color(ushort value) =>
            spawn.AddValue("Other", "Color", value);
        public void Other_Ip(string value) =>
            spawn.AddValue("Other", "Ip", value);
        public void Other_IsSpectator(bool value) =>
            spawn.AddValue("Other", "IsSpectator", value);
        public void Other_Name(string value) =>
            spawn.AddValue("Other", "Name", value);
        public void Other_Port(ushort value) =>
            spawn.AddValue("Other", "Port", value);
        public void Other_Side(ushort value) =>
            spawn.AddValue("Other", "Side", value);
        #endregion
        /// <summary>
        /// Multi*_Alliances
        /// </summary>
        /// <exception cref="RA2Library.Exception.SpawnException.AllyOverFlowException">盟友溢出异常</exception>
        public struct Alliances
        {
            /// <summary>
            /// 是否使用
            /// </summary>
            public bool Used { get; private set; } // is include
            public byte Player { get; private set; }
            public ushort? HouseAllyOne { get; private set; }
            public ushort? HouseAllyTwe { get; private set; }
            public ushort? HouseAllyThree { get; private set; }
            public ushort? HouseAllyFour { get; private set; }
            public ushort? HouseAllyFive { get; private set; }
            public ushort? HouseAllySix { get; private set; }
            public ushort? HouseAllySeven { get; private set; }
            public Alliances(byte player)
            {
                Player = player;
                HouseAllyOne = 9;
                HouseAllyTwe = 9;
                HouseAllyThree = 9;
                HouseAllyFour = 9;
                HouseAllyFive = 9;
                HouseAllySix = 9;
                HouseAllySeven = 9;
                Used = false;
            }
            public void Add(int player)
            {
                Used = true;
                byte Player = (byte)player;
                if (HouseAllyOne != 9)
                    HouseAllyOne = Player;
                else if (HouseAllyTwe != 9)
                    HouseAllyTwe = Player;
                else if (HouseAllyThree != 9)
                    HouseAllyThree = Player;
                else if (HouseAllyFour != 9)
                    HouseAllyFour = Player;
                else if (HouseAllyFive != 9)
                    HouseAllyFive = Player;
                else if (HouseAllySix != 9)
                    HouseAllySix = Player;
                else if (HouseAllySeven != 9)
                    HouseAllySeven = Player;
                else throw new AllyOverFlowException("Alliances溢出");
            }
        }
        /// <summary>
        /// 创建Spawn文件
        /// </summary>
        public void Make()
        {
            List<Alliances> alliances = new List<Alliances>();
            if (Multi1_Alliances.Used) alliances.Add(Multi1_Alliances);
            if (Multi2_Alliances.Used) alliances.Add(Multi2_Alliances);
            if (Multi3_Alliances.Used) alliances.Add(Multi3_Alliances);
            if (Multi4_Alliances.Used) alliances.Add(Multi4_Alliances);
            if (Multi5_Alliances.Used) alliances.Add(Multi5_Alliances);
            if (Multi6_Alliances.Used) alliances.Add(Multi6_Alliances);
            if (Multi7_Alliances.Used) alliances.Add(Multi7_Alliances);
            if (Multi8_Alliances.Used) alliances.Add(Multi8_Alliances);
            foreach (Alliances ally in alliances)
            {
                MemoryIni.Section al = new MemoryIni.Section(
                    "Multi" + ally.Player.ToString() + "_Alliances");
                if (ally.HouseAllyOne != 9)
                    al.Add("HouseAllyOne", "Multi" + ally.HouseAllyOne.ToString());
                if (ally.HouseAllyTwe != 9)
                    al.Add("HouseAllyTwe", "Multi" + ally.HouseAllyTwe.ToString());
                if (ally.HouseAllyThree != 9)
                    al.Add("HouseAllyThree", "Multi" + ally.HouseAllyThree.ToString());
                if (ally.HouseAllyFour != 9)
                    al.Add("HouseAllyFour", "Multi" + ally.HouseAllyFour.ToString());
                if (ally.HouseAllyFive != 9)
                    al.Add("HouseAllyFive", "Multi" + ally.HouseAllyFive.ToString());
                if (ally.HouseAllySix != 9)
                    al.Add("HouseAllySix", "Multi" + ally.HouseAllySix.ToString());
                if (ally.HouseAllySeven != 9)
                    al.Add("HouseAllySeven", "Multi" + ally.HouseAllySeven.ToString());
                spawn.Add(al);
            }
            spawn.SaveToFileNoSummary(Path);
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        public Spawn()
        {
            this.Path = System.AppDomain.CurrentDomain.BaseDirectory + "spawn.ini";
            spawn = new IniFile();
            Multi1_Alliances = new Alliances(1);
            Multi2_Alliances = new Alliances(2);
            Multi3_Alliances = new Alliances(3);
            Multi4_Alliances = new Alliances(4);
            Multi5_Alliances = new Alliances(5);
            Multi6_Alliances = new Alliances(6);
            Multi7_Alliances = new Alliances(7);
            Multi8_Alliances = new Alliances(8);
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="Path">spawn.ini 文件位置</param>
        public Spawn(string Path):this()
        {
            this.Path = Path;
        }


        /// <summary>
        /// 盟友信息溢出异常
        /// </summary>
        [System.Serializable]
        public class AllyOverFlowException : System.Exception
        {
            public AllyOverFlowException() { }
            public AllyOverFlowException(string message) : base(message) { }
            public AllyOverFlowException(string message, System.Exception inner) : base(message, inner) { }
            protected AllyOverFlowException(
              System.Runtime.Serialization.SerializationInfo info,
              System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
        }
    }
}
