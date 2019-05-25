using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;

namespace DynamicDataWebSite.DynamicData.FieldTemplates
{
    public partial class ImagePathUpload_Edit : System.Web.DynamicData.FieldTemplateUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Mode == DataBoundControlMode.Edit)
                photoTable.Visible = true;
            else
                photoTable.Visible = false;
        }
        /// <summary>
        /// big old doodle the upload it self
        /// takes the files stores them compines the names and sends it backto viewstate
        /// </summary>
        public void Upload()
        {
            string retpath = "";
            List<HttpPostedFile> files = new List<HttpPostedFile>();
            for (int i = 0; i < 4; i++)
            {
                if (i <= fileupload1.PostedFiles.Count - 1)
                    files.Add(fileupload1.PostedFiles[i]);
            }

            string uploadfolder = "";
            var col = Column.Attributes.OfType<DynamicDataLibrary.Attributes.CustomViewAttribute>().FirstOrDefault();
            if (col != null)
                uploadfolder = col.CustomText;
            else
                uploadfolder = "../../RealEstate/PropertyImage/";
            foreach (var item in files)
            {
                string name = GetName(Server.MapPath(uploadfolder));
                name = name + System.IO.Path.GetExtension(item.FileName);
                item.SaveAs(Server.MapPath(uploadfolder) + name);
                retpath += name + "|";
            }
            Path = retpath;
            //retpath = "";
            ViewState.Add("Path", retpath);
        }
        string path;
        public override Control DataControl
        {
            get
            {
                return up1;
            }
        }
        public string Path
        {
            get
            {
                return path;
            }

            set
            {
                path = value;
            }
        }

        public string OldPath
        {
            get
            {
                return oldPath;
            }

            set
            {
                oldPath = value;
            }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            base.OnDataBinding(e);
            //for image view
            OldPath = FieldValueString;
            if (Mode == DataBoundControlMode.Edit &&!string.IsNullOrEmpty(OldPath))
            {
                List<string> ls = ImagePathUpload.GetFileNames(OldPath, this.Column);
                string uploadfolder = "";
                var col = Column.Attributes.OfType<DynamicDataLibrary.Attributes.CustomViewAttribute>().FirstOrDefault();
                if (col != null)
                    uploadfolder = col.CustomText;
                else
                    uploadfolder = "../../RealEstate/PropertyImage/";
                foreach (var item in ls)
                {
                    TableCell c = new TableCell();
                    c.Controls.Add(new Image { ImageUrl = uploadfolder + item, Width = 200, Height = 150 });
                    photoTable.Rows[0].Cells.Add(c);
                }
            }
        }
        string oldPath;
        protected override void ExtractValues(IOrderedDictionary dictionary)
        {
            base.ExtractValues(dictionary);
            string val = "";
            if (Path == null || Path == "")
            {
                if (ViewState.Count > 0)
                    val = ViewState["Path"].ToString();
            }
            else
            {
                val = path;
            }
            dictionary[Column.Name] = val;

        }
        /// <summary>
        /// generats random string -_-
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        /// <summary>
        /// returns a random non repeated name for the image
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string GetName(string path)
        {
            string[] s = Directory.GetFiles(path);
            string name = RandomString(15);
            for (int i = 0; i < s.Length; i++)
                if (name == s[i])
                {
                    name = RandomString(15); i = 0;
                }
            return name;
        }
        /// <summary>
        /// checks if it is actualy an image
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        public bool IsImage(HttpPostedFile f)
        {
            string ext = System.IO.Path.GetExtension(f.FileName).ToLower();
            if (ext == ".jpg" || ext == ".png" || ext == ".bmp")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        protected void Unnamed_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //  args.IsValid = CheckImages();
        }
        /// <summary>
        /// checks the images for any other file type
        /// additional security for upload can be placed here
        /// </summary>
        /// <returns></returns>
        public bool CheckImages()
        {
            if (!fileupload1.HasFile)
            {
                return false;
            }
            foreach (var item in fileupload1.PostedFiles)
            {
                if (!IsImage(item))
                    return false;
            }
            return true;
        }
        /// <summary>
        /// check the images upload first 4 and set the path
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnUpload_Click(object sender, EventArgs e)
        {
            if (CheckImages())
            {
                BtnUpload.Enabled = false;
                BtnUpload.CssClass = "button2d";
                BtnUpload.Text = Resources.RealEstate.Image_Uploaded;
                BtnCancel.ForeColor = System.Drawing.Color.Red;
                BtnCancel.Enabled = true;
                errorLabel.Visible = false;
                ErrVal.IsValid = true;
                Upload();
            }
            else
            {
                errorLabel.Visible = true;
                ErrVal.IsValid = false;
            }
        }

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            BtnUpload.Enabled = true;
            BtnUpload.CssClass = "button2";
            BtnUpload.Text = Resources.RealEstate.Image_Upload;
            BtnCancel.ForeColor = System.Drawing.Color.Gray;
            BtnCancel.Enabled = false;
            ClearImages();
        }
        /// <summary>
        /// clear the current list of files and the path in the viewstate
        /// </summary>
        public void ClearImages()
        {
            string pth = !string.IsNullOrEmpty(Path) ? Path : ViewState["Path"].ToString();
            List<string> paths = ImagePathUpload.GetFileNames(pth, Column);
            if (paths != null)
                foreach (var item in paths)
                {
                    string s = Server.MapPath(item);
                    if (File.Exists(s))
                        File.Delete(s);
                }
            Path = null;
            ViewState.Remove("Path");
        }
    }
}