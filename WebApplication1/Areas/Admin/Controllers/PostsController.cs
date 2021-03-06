﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataLib;
using DataLib.Models;
using WebApplication1.Areas.Admin.ViewModels;
using WebApplication1.Areas.Admin.Helpers;
using System.Text.RegularExpressions;
using WebApplication1.Helpers;

namespace WebApplication1.Areas.Admin.Controllers
{
    public class PostsController : AdminController
    {
        private readonly MyContext ctx;

        public PostsController(MyContext db)
        {
            ctx = db;
        }

        public IActionResult Index()
        {
            return View(PreparePosts());
        }

        public IActionResult Create(int? catId)
        {
            return PartialView("Edit", new PostEditVM
            {
                Categories = AllCategories(),
                CategoryId = catId != null ? (int)catId : 0,
                PredefinedCategory = catId != null ? true : false
            });
        }

        public IActionResult Edit(int id)
        {
            Post x = ctx.Posts.Find(id);
            return PartialView(new PostEditVM
            {
                PostId = x.PostID,
                Title = x.Title,
                Description = x.Description,
                Body = x.Body,
                CategoryId = x.Category.CategoryID,
                Categories = AllCategories()
            });
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(PostEditVM x)
        {
            if (!ModelState.IsValid)
            {
                x.Categories = AllCategories();
                return PartialView("Edit", x);
            }

            Post post;
            if (x.PostId == 0)
            {
                post = new Post();
                ctx.Posts.Add(post);
            }
            else
            {
                post = ctx.Posts.Find(x.PostId);
            }

            post.Title = x.Title;
            post.Description = x.Description;
            post.Body = x.Body;
            post.Category = ctx.Categories.Find(x.CategoryId);
            //post.Moderator = HttpContext.GetLogiranogModeratora();

            ctx.SaveChanges();

            if (x.PredefinedCategory)
            {
                return RedirectToAction("IndexPartial", "Category", new { area = "Admin" });
            }

            return PartialView("Index", PreparePosts());
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await ctx.Posts
                .FirstOrDefaultAsync(m => m.PostID == id);
            if (post == null)
            {
                return NotFound();
            }
            ctx.Posts.Remove(post);
            await ctx.SaveChangesAsync();

            return PartialView(nameof(Index), PreparePosts());
        }

        #region HelpersMethods
        private PostListVM PreparePosts()
        {
            return new PostListVM
            {
                Items = ctx.Posts.Include(p => p.Category).ToList()
                    .Select(p => {
                        var itm = new PostListVM.ItemInfo();
                        itm.Title = p.Title;
                        itm.PostId = p.PostID;
                        itm.CategoryName = p.Category?.Name;
                        itm.Description = p.Description;
                        if (p.Body != null)
                        {
                            itm.Body = p.Body.Length > 50 ?
                                Regex.Replace(p.Body, "<.*?>", String.Empty).Substring(0, 50) :
                                Regex.Replace(p.Body, "<.*?>", String.Empty);
                        } else
                        {
                            itm.Body = "-";
                        }

                        return itm;
                    })
                    .ToList()
            };
        }

        private List<SelectListItem> AllCategories()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.AddRange(ctx.Categories.Select(x => new SelectListItem
            {
                Value = x.CategoryID.ToString(),
                Text = x.Name
            }));
            return list;
        }

        private bool PostExists(int id)
        {
            return ctx.Posts.Any(e => e.PostID == id);
        }
        #endregion
    }
}
