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
            if (Session["rental.user"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        public ActionResult GetNavData()
        {
            List<TreeNode> nodes = new List<TreeNode>()
            {
                new TreeNode()
                {
                    Text = "房间管理",
                    IconCls = "pic_26",
                    Children = new List<TreeNode>()
                    {
                        new TreeNode() { Text = "房间管理", IconCls = "pic_93", Url = Url.Action("Index", "Room") },
                        new TreeNode() { Text = "美食管理", IconCls = "pic_93", Url = Url.Action("Index", "Food") },
                        new TreeNode() { Text = "优惠精选", IconCls = "pic_93", Url = Url.Action("Index", "Preference") },
                         
                    }
                },
                new TreeNode()
                {
                    Text = "首页内容管理",
                    IconCls = "pic_100",
                    Children = new List<TreeNode>()
                    {
                        new TreeNode() { Text = "酒店介绍", IconCls = "pic_93", Url = Url.Action("Create", "About") },
                        new TreeNode() { Text = "幻灯片管理", IconCls = "pic_5", Url = Url.Action("Index", "Slider") },
                        new TreeNode() { Text = "服务设置", IconCls = "pic_198", Url = Url.Action("Index", "Service") },
                    }
                },
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
