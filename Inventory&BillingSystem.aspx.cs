using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace Inventory_BillingSystem
{
	public partial class Inventory_BillingSystem : System.Web.UI.Page
	{
		SqlConnection con;
		SqlCommand cmd;
		SqlDataAdapter sda;
		DataSet ds;

		protected void Page_Load(object sender, EventArgs e)
		{
			con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=A:\\ASP.NET\\InventoryBillingSystem\\InventoryBillingSystem\\App_Data\\Database1.mdf;Integrated Security=True");
		}

		protected void show_grid_data()
		{
			con.Open();

			sda = new SqlDataAdapter("select * from Items", con);
			ds = new DataSet();
			sda.Fill(ds);

			GridView1.DataSource = ds.Tables[0];
			GridView1.DataBind();

			con.Close();
		}

		protected void Button1_Click(object sender, EventArgs e)
		{
			show_grid_data();
		}

		protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
		{
			GridView1.EditIndex = e.NewEditIndex;
			show_grid_data();
		}

		protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
		{
			GridViewRow rowid = GridView1.Rows[e.RowIndex];

			int id = Convert.ToInt32(
				((TextBox)rowid.Cells[1].Controls[0]).Text
			);

			string name = ((TextBox)rowid.Cells[2].Controls[0]).Text;

			string category = ((TextBox)rowid.Cells[3].Controls[0]).Text;

			int price = Convert.ToInt32(
				((TextBox)rowid.Cells[4].Controls[0]).Text
			);

			int quantity = Convert.ToInt32(
				((TextBox)rowid.Cells[5].Controls[0]).Text
			);

			con.Open();

			cmd = new SqlCommand("update Items set item_name='" + name +"', category='" + category +"', price=" + price +", quantity=" + quantity +" where item_id=" + id,con);

			cmd.ExecuteNonQuery();

			con.Close();

			GridView1.EditIndex = -1;
			show_grid_data();
		}

		protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
		{
			GridView1.EditIndex = -1;
			show_grid_data();
		}

		protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
		{
			GridViewRow rowid = GridView1.Rows[e.RowIndex];

			int id = Convert.ToInt32(
				((TextBox)rowid.Cells[1].Controls[0]).Text
			);

			con.Open();

			cmd = new SqlCommand("delete from Items where item_id=" + id,con);

			cmd.ExecuteNonQuery();

			con.Close();

			show_grid_data();
		}
	}
}