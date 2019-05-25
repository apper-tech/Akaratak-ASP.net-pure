<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Carrer.aspx.cs" Inherits="DynamicDataWebSite.Carrer" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="career">
        <div class="container">
            <h3 style="font-family: 'Segoe UI'"><% = Page.Title %></h3>
            <asp:PlaceHolder runat="server" ID="eng">
                <style>
                    li i {
                        position: relative;
                        left:-10px;
                    }
                </style>
                    <p style="font-size: 30px">Digital Marketing Specialist</p>
                    <p>We are looking for markiting specialist to work online in syria full time job.</p>
                    <h4>Tasks my include:</h4>
                    <ul class="career-start">
                        <li><a href="#"><i></i>Provide consultation and development plan for the company's websites regarding the design, contents and user experience.</a></li>
                        <li><a href="#"><i></i>Add and Edit the Website content (English & Arabic).</a></li>
                        <li><a href="#"><i></i>Prepare studies for clients orientations and Market Developments.</a></li>       
                        <li><a href="#"><i></i>Prepare Studies about competitors</a></li>
                        <li><a href="#"><i></i>Prepare and edit the company pages in social media (Facebook, Twitter , LinkedIn...)</a></li>
                        <li><a href="#"><i></i>Execute And Maintain Digital Advertisements and Promotions</a></li>
                        <li><a href="#"><i></i>Generate Special Reports Using about users usage and visits using Google Analytics </a></li>
                        <li><a href="#"><i></i>Develop and Analys Search engine data (SEO) To insure it's best usage for the development of the Site Map.</a></li>
                        <li><a href="#"><i></i>Review and test the e-services new features.</a></li>
                        <li><a href="#"><i></i>Sell The company products online and provide services and answer clients question on the websites.</a></li>

                        <li>
                            <h5>_________________________________________</h5>
                        </li>
                    </ul>
                    <p>(Note: we are looking for an expert to advise us with his experience and tell us what to do)</p>
                    <p>And one of the most important things to have is the ability for fast learning of new concepts and technique and the ability to apply them with proficiency</p>
                    <p>The work primarily will be from home and the employee must provide internet and computer, the work hours is fixable as the employee must achieve 45 hours a week or 9 a day for 5 days </p>
                    <p>Ability to complete the hours is requiring as its a full time job (can start with part time job for 4-month maximum)</p>
                    <h4 style="font-family: 'Segoe UI'">Please send your CV to : maltabba@Apper Tech.com. (Add  "JOB1000003 Digital Marketing - Syria (S)" to the email subject)</h4>
            </asp:PlaceHolder>
            <asp:PlaceHolder runat="server" ID="arb">
                <style>
                    li i {
                        position: relative;
                        left: 10px;
                    }
                </style>
                <div dir="rtl">
                    <p style="font-size: 30px">اختصاصي تسويق إلكتروني</p>
                    <p>نبحث عن اختصاصي تسويق إلكتروني في سوريا خبرة سنة على الأقل ليعمل بدوام كامل عن بُعد.</p>
                    <h4>المهام يمكن أن تشمل:</h4>
                    <ul class="career-start" dir="rtl">
                        <li><a href="#"><i></i>تقديم استشارات وخطط تطوير للمواقع الالكترونية للشركة من حيث التصميم والمحتوى وسهولة الاستخدام.</a></li>
                        <li><a href="#"><i></i>إضافة وتحرير محتوى الموقع الالكتروني (باللغة العربية والإنكليزية).</a></li>
                        <li><a href="#"><i></i>إعداد الدراسات الخاصة بتوجهات العملاء والتطورات التي يشهدها السوق.</a></li>
                        <li><a href="#"><i></i>إعداد الدراسات حول المنافسين.</a></li>
                        <li><a href="#"><i></i>إعداد وتحرير الصفحات الخاصة بالشركة على وسائل الإعلام (Facebook, Twitter , LinkedIn...)</a></li>
                        <li><a href="#"><i></i>تنفيذ ومتابعة الحملات الإعلانية والترويجية الالكترونية.</a></li>
                        <li><a href="#"><i></i>استخراج تقارير خاصة باستخدام و زيارة العملاء بإستخدام Google Analytics </a></li>
                        <li><a href="#"><i></i>تطوير و تحليل بيانات محركات البحث (SEO) لضمان استغلالها بالشكل الأمثل و استخدامها لتطوير خريطة الموقع (Site Map).</a></li>
                        <li><a href="#"><i></i>مراجعة واختبار الميزات الجديدة للخدمات الإلكترونية للشركة.</a></li>
                        <li><a href="#"><i></i>بيع منتجات الشركة الكترونيا وتقديم خدماتها والرد على استفسارات العملاء على المواقع الالكترونية.</a></li>
                        <li>
                            <h5>_________________________________________</h5>
                        </li>
                    </ul>
                    <p>(ملاحظة: نحن نبحث عن صاحب خبرة ليشير عليها بحسب خبرته ما هو الأفضل لنفعله)</p>
                    <p>وإن من اهم الأشياء الواجب توافرها هو أمكانية التعلم السريع للمفاهيم والتقنيات الجديدة والقدرة على تطبيقهم بشكل متقن</p>
                    <p>العمل سيكون مبدئيا من المنزل ويجب توفير الجهاز والإنترنت حاليا من قِبَل الموظف، والدوام مرن بحيث يجب تحقيق 45 ساعة في الأسبوع بواقع 9 ساعات وسطيا لمدة 5 أيام، ويشترط التفرغ التام. (يمكن مبدئيا العمل لفترة تجريبية بدوام جزئي لكن بعد 3 أشهر كحد أفصى يجب التفرغ)</p>
                    <h4 style="font-family: 'Segoe UI'">الرجاء إرسال السيرة الذاتية إلى: maltabba@Apper Tech.com. (أضف "JOB1000003 Digital Marketing - Syria (S)" إلى عنوان البريد الإلكتروني)</h4>
                </div>
            </asp:PlaceHolder>
        </div>
    </div>
</asp:Content>
