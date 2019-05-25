using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace DynamicDataWebSite.DynamicData.FieldTemplates
{
    public partial class ImagePathUpload : System.Web.DynamicData.FieldTemplateUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        string path;
        string im1;
        string im2;
        string im3;
        string im4;

        public string Im1
        {
            get
            {
                return im1;
            }

            set
            {
                im1 = value;
            }
        }

        public string Im2
        {
            get
            {
                return im2;
            }

            set
            {
                im2 = value;
            }
        }

        public string Im3
        {
            get
            {
                return im3;
            }

            set
            {
                im3 = value;
            }
        }

        public string Im4
        {
            get
            {
                return im4;
            }

            set
            {
                im4 = value;
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

        protected override void OnDataBinding(EventArgs e)
        {
            base.OnDataBinding(e);
            Path = FieldValueString;
           //check for no image , set the default sample image
            if (Path != "")
            {
                //convert the pipline seperated file names to a list
                List<string> ls = GetFileNames(path,Column);

                if (ls != null && ls.Count > 0)
                   
                    //check for list or details for slider or image
                    if (System.IO.Path.GetFileName(Page.AppRelativeVirtualPath).Contains("List"))
                    {
                        //if list hide details
                        DetailsHolder.Visible = false;
                        bool exc = false;
                        //check if one of the files exist
                        foreach (var item in ls)
                        {
                            if (!item.Contains("NUL")&& File.Exists(Server.MapPath(item)))
                            {
                                ///if does assign and exit
                                exc = true;
                                im1 = item;
                                break;
                            }
                        }
                        if (!exc)
                            ///set default 
                            SetDefaultImage();
                    }
                    else
                    {
                        /// if details hide list
                        ListHolder.Visible = false;
                        //assign the first image var to list first
                        im1 = ls[0];
                        ///add meta tags to images according to schema.org
                        HtmlMeta meta = new HtmlMeta();
                        meta.Name = "og:image";
                        meta.Content ="http://"+ Request.Url.Host + "/" + im1.Remove(0, 6);
                        this.Page.Header.Controls.Add(meta);
                        //assign images accorfing to number
                        if (ls.Count > 1)
                        {
                            im2 = ls[1];
                        }
                        if (ls.Count > 2)
                        {
                            im3 = ls[2];
                        }
                        if (ls.Count > 3)
                        {
                            im4 = ls[3];
                        }
                       //if any image is Not found set it as def
                        if (string.IsNullOrEmpty(im1))
                        {
                            //get default Image
                            SetDefaultImage();
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(im2) || ! File.Exists(im2))
                                Im2 = Im1;
                            if (string.IsNullOrEmpty(im3) || !File.Exists(im3))
                                Im3 = Im1;
                            if (string.IsNullOrEmpty(im4) || !File.Exists(im2))
                                Im4 = Im1;
                        }

                    }
            }
            else
            {
                //if no path just set defult
                if (System.IO.Path.GetFileName(Page.AppRelativeVirtualPath).Contains("List"))
                {
                    DetailsHolder.Visible = false;
                }
                else
                {
                    ListHolder.Visible = false;
                }
                //get default Image
                SetDefaultImage();
            }
        }
        public void SetDefaultImage()
        {
            string path1 = "/CustomDesign/images/defslider.jpg";
            string path2 = "/CustomDesign/images/defsliderlist.jpg";
            Im2 = Im3 = Im4 = path1;
            if (System.IO.Path.GetFileName(Page.AppRelativeVirtualPath).Contains("List"))
                Im1 = path2;
            else
                Im1 = path1;
        }
        /// <summary>
        /// splits each image name removes chars and appends full path using meta tags
        /// </summary>
        /// <param name="Path">the DB image path</param>
        /// <param name="col"> the meta collum for image attribute</param>
        /// <returns> string list of images for editing</returns>
        public static List<string> GetFileNames(string Path,System.Web.DynamicData.MetaColumn col)
        {

            if (!string.IsNullOrEmpty(Path))
            {
                Path = Path.Remove(Path.Length - 1, 1);
                string temp = "";
                List<string> res = new List<string>();
                for (int i = 0; i < Path.Length; i++)
                {
                    if (Path[i] == '|')
                    {
                        res.Add(temp);
                        Path = Path.Substring(i + 1);
                        i = 0;
                        temp = "";
                    }
                    temp += Path[i];
                }
                res.Add(temp);
                DynamicDataLibrary.Attributes.CustomViewAttribute path = null;
                if (col != null)
                    path = col.Attributes.OfType<DynamicDataLibrary.Attributes.CustomViewAttribute>().FirstOrDefault();
                string uploadfolder = "";
                if (path != null)
                    uploadfolder = path.CustomText;
                else
                    uploadfolder = "/RealEstate/PropertyImage/";
                for (int i = 0; i < res.Count; i++)
                {
                    res[i] = uploadfolder + res[i];
                }
                return res;
            }
            return null;
        }
        protected override void ExtractValues(IOrderedDictionary dictionary)
        {
            base.ExtractValues(dictionary);
        }
    }
}