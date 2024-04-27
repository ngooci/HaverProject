﻿namespace HaverProject.Models
{
    public class ItemMarked
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<NCR> Ncrs { get; set; } = new HashSet<NCR>();
    }
}
