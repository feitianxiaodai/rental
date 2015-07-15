using Rental.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rental.UI.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetNavData()
        {
            List<TreeNode> nodes = new List<TreeNode>()
            {
                new TreeNode()
                {
                    Text = "布局",
                    IconCls = "pic_26",
                    Children = new List<TreeNode>()
                    {
                        new TreeNode() { Text = "幻灯片管理", IconCls = "pic_5", Url = Url.Action("Index", "Slider") },
                        new TreeNode() { Text = "角色管理", IconCls = "pic_198", Url = Url.Action("Index", "Roles") },
                        new TreeNode() { Text = "房间管理", IconCls = "pic_93", Url = Url.Action("CreateStep1", "Room") },
                    }
                },
                new TreeNode()
                {
                    Text = "系统",
                    IconCls = "pic_100",
                    Children = new List<TreeNode>()
                    {
                        new TreeNode() { Text = "操作日志", IconCls = "pic_125", Url = Url.Action("Index", "OperateLogs") },
                        new TreeNode() { Text = "系统日志", IconCls = "pic_101", Url = Url.Action("Index", "SystemLogs") },
                        new TreeNode() { Text = "系统设置", IconCls = "pic_89", Url = Url.Action("Index", "SystemSettings") }
                    }
                }
            };

            Action<ICollection<TreeNode>> action = list =>
            {
                foreach (var node in list)
                {
                    node.Id = "node" + node.Text;
                }
            };

            foreach (var node in nodes)
            {
                node.Id = "node" + node.Text;
                if (node.Children != null && node.Children.Count > 0)
                {
                    action(node.Children);
                }
            }

            return Json(nodes, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Welcome()
        {
            return View();
        }

    }
}
