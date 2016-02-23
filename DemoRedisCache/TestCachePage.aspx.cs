using DemoRedisCache.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DemoRedisCache
{
    public partial class TestCachePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var list = CacheManager.GetFruitListFromCache();

                if (list.Count > 0)
                {
                    GridView1.DataSource = list;
                    GridView1.DataBind();
                }
            }
        }

        protected void ButDelete_Click(object sender, EventArgs e)
        {
            CacheManager.DeleteFruitList();
        }
    }
}