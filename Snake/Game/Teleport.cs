namespace Snake.Game
{
    public class Teleport
    {
        public string GetNameTeleportTo(string name)
        {
            int startTeleportFrom = name.IndexOf(';') + 1;
            int startTeleportTo = name.IndexOf(';', startTeleportFrom) + 1;

            string teleportNumberFrom = name.Substring(startTeleportFrom, startTeleportTo - startTeleportFrom - 1);
            string teleportNumberTo = name.Substring(startTeleportTo, name.Length - startTeleportTo);

            int.TryParse(teleportNumberFrom, out int teleportFromNumber);
            int.TryParse(teleportNumberTo, out int teleportToNumber);
            return "Teleport;" + teleportToNumber + ";" + teleportFromNumber;
        }
    }
}
