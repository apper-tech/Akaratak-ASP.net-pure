<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Loader.ascx.cs" Inherits="DynamicDataWebSite.ServerControls.Loader" %>
 <div id="divLoader" style="display: none;">
                    <style>
                        #fade {
                            display: none;
                            position: absolute;
                            top: 0%;
                            left: 0%;
                            width: 100%;
                            height: 100%;
                            background-color: #ababab;
                            z-index: 1001;
                            -moz-opacity: 0.8;
                            opacity: .70;
                            filter: alpha(opacity=80);
                        }

                        #modal {
                            display: none;
                            position: absolute;
                            top: 45%;
                            left: 45%;
                            padding: 30px 15px 0px;
                            border: 3px solid #ababab;
                            box-shadow: 1px 1px 10px #ababab;
                            border-radius: 20px;
                            background-color: white;
                            z-index: 1002;
                            text-align: center;
                            overflow: auto;
                        }
                    </style>
                    <div id="fade" style="display: block;"></div>
                    <div id="modal" style="display: block;">
                        <img id="loader" src="/CustomDesign/images/loading.gif">
                    </div>
                </div>
                <script>
                    Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
                    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);

                    function BeginRequestHandler(sender, args) {
                        // Show the mask
                        document.body.style.overflow = 'hidden';
                        $find('bMPLoad').show();
                    }

                    function EndRequestHandler(sender, args) {
                        // Hide the mask
                        document.body.style.overflow = 'scroll';
                        $find('bMPLoad').hide();
                    }

                </script>