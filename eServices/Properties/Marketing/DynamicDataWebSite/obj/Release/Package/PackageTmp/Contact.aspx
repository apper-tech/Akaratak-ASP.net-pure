<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="DynamicDataWebSite.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="contact">
        <div class="container">
            <h3><%=Page.Title %></h3>
            <asp:PlaceHolder runat="server" ID="arb">
                <div class="contact-top">
                    <div class="col-md-6 contact-top1">
                        <h4>معلومات</h4>
                        <p class="text-contact">
                            هي شركة تقدم خدمات عامة للمستخدمين بعدة مجالات
                        حيث ان اول خدماتنا هي العقارات وفي تطور للمزيد من الخدمات
                        </p>
                        <div class="contact-address">
                            <div class="col-md-6 contact-address1">
                                <p><b>Apper Tech</b></p>
                            </div>
                            <div class="col-md-6 contact-address1">
                                <h5>البريد الالكتروني</h5>
                                <p>عام :<a href="malito:info@akaratak.com"> info(at)akaratak.com</a></p>
                                <p>الدعم :<a href="malito:support@akaratak.com"> support(at)akaratak.com</a></p>
                            </div>
                            <div class="clearfix"></div>
                        </div>
                        <div class="contact-address">
                            <div class="clearfix"></div>
                        </div>
                    </div>
                    <div class="col-md-6 contact-right">
                        <input class="fontstyled2" id="name" type="text" placeholder="الاسم" required="" />
                        <input class="fontstyled2" id="email" type="text" placeholder="البريد الالكتروني" required="" />
                        <input class="fontstyled2" id="supject" type="text" placeholder="الموضوع" required="" />
                        <textarea id="msg" placeholder="الرسالة"></textarea>
                        <label class="hvr-sweep-to-right">
                            <input class="fontstyled2" type="submit" value="أرسل" onclick="Send()" />
                        </label>
                    </div>
                    <div class="clearfix"></div>
                </div>
            </asp:PlaceHolder>
            <asp:PlaceHolder runat="server" ID="eng">
                <div class="contact-top">
                    <div class="col-md-6 contact-top1">
                        <h4>Information</h4>
                        <p class="text-contact">
                           It is a company that provide public services for users in several areas
                         Since the first of our services are real estate and in the development of more services
                        </p>
                        <div class="contact-address">
                            <div class="col-md-6 contact-address1">
                                
                                <p><b>Apper Tech</b></p>
                            </div>
                            <div class="col-md-6 contact-address1">
                                <h5>Email:</h5>
                                <p>General :<a href="malito:info@akaratak.com"> info(at)akaratak.com</a></p>
                                <p>Support :<a href="malito:support@akaratak.com"> support(at)akaratak.com</a></p>
                            </div>
                            <div class="clearfix"></div>
                        </div>
                        <div class="contact-address">
                            <div class="clearfix"></div>
                        </div>
                    </div>
                    <div class="col-md-6 contact-right">
                        <input class="fontstyled2" class="fontstyled2" id="name" type="text" placeholder="Name" required="" />
                        <input class="fontstyled2" id="email" type="text" placeholder="Email Address" required="" />
                        <input class="fontstyled2" id="supject" type="text" placeholder="Subject" required="" />
                        <textarea id="msg" placeholder="Message"></textarea>
                        <label class="hvr-sweep-to-right">
                            <input class="fontstyled2" type="submit" value="Send" onclick="Send()" />
                        </label>
                    </div>
                    <div class="clearfix"></div>
                </div>
            </asp:PlaceHolder>
            <script>
                function Send() {
                    var body = "";
                    var name = document.getElementById('name').value;
                    var email = document.getElementById('email').value;
                    var sup = document.getElementById('supject').value;
                    var msg = document.getElementById('msg').value;
                    body = "My Name is : " + name + "%0D%0A And My Email is : " + email + "%0D%0A I Want to : " + msg;
                    window.open('mailto:info@akaratak.com?subject=' + sup + '&body=' + body);
                }
            </script>
        </div>
        <div class="map">
            <div style="position: center"><center>
                <img src="CustomDesign/images/at.jpg" />
                <hr />
                <h4>Apper Tech</h4>
            </div>
            <div class="clearfix"></div></center>
            <br />
            <br />
        </div>
    </div>
</asp:Content>
