using AwesomeLampApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeLampApp.ViewModels
{
    public class MoreViewModel
    {
        public List<ListItemModel> Optionlist { get; set; }

        public MoreViewModel()
        {
            Optionlist = new List<ListItemModel>();
            Optionlist.Add(new ListItemModel { Title = "疲劳检测", Description = "" });
            Optionlist.Add(new ListItemModel { Title = "智能场景切换", Description = "识别不同使用场景，自动调整灯光。" });
            Optionlist.Add(new ListItemModel { Title = "个性语音提醒", Description = "" });
            Optionlist.Add(new ListItemModel { Title = "情绪分析", Description = "" });
            Optionlist.Add(new ListItemModel { Title = "番茄灯", Description = "番茄钟+台灯" });
            Optionlist.Add(new ListItemModel { Title = "更多设置", Description = "" });
        }
    }
}
