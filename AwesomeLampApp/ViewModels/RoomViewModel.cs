using AwesomeLampApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeLampApp.ViewModels
{
    public class RoomViewModel
    {
        public List<ListItemModel> Memberlist { get; set; }

        public RoomViewModel()
        {
            Memberlist = new List<ListItemModel>();
            Memberlist.Add(new ListItemModel { Rank = "1", ImageUrl = "t01.webp", Name = "Jack", Description = "Keep going!", Time = "128"});
            Memberlist.Add(new ListItemModel { Rank = "2", ImageUrl = "t02.webp", Name = "Alice", Description = "I can do it!", Time = "88" });
            Memberlist.Add(new ListItemModel { Rank = "3", ImageUrl = "t03.webp", Name = "Tom", Description = "Fighting~", Time = "43" });
            Memberlist.Add(new ListItemModel { Rank = "4", ImageUrl = "t04.webp", Name = "Jason", Description = "Orz", Time = "23" });
        }
    }
}
