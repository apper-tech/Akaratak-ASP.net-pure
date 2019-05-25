<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="DynamicDataWebSite.Autocomplete_Filter" CodeBehind="Autocomplete.ascx.cs" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<script type="text/javascript">
    function OnClientPopulating(sender, e) {
        sender._element.className = "DDFilter_Loading";
    }
    function OnClientCompleted(sender, e) {
        sender._element.className = "DDFilter";
    }
</script>

<div style="display: inline-block;">
    <div href="#" alt="ادخل الاسم ، أو رقم الهوية للبحث ..." class="tooltip">
        <asp:TextBox runat="server" ID="AutocompleteTextBox" autocomplete="off" CssClass="DDFilter" />
    </div>
    <asp:HiddenField
        runat="server"
        ID="AutocompleteValue"
        OnValueChanged="AutocompleteValue_ValueChanged" />
    <asp:Button runat="server" ID="ClearButton" OnClick="ClearButton_Click" Text="<%$Resources:DynamicData, Clear %>" CssClass="DDFilter" />

    <ajaxToolkit:AutoCompleteExtender
        runat="server"
        BehaviorID="AutoCompleteBehaviorIDPlaceholder"
        ID="autoComplete1"
        TargetControlID="AutocompleteTextBox"
        ServicePath="~\AutocompleteFilter.asmx"
        ServiceMethod="GetCompletionList"
        MinimumPrefixLength="1"
        CompletionInterval="1000"
        EnableCaching="false"
        CompletionSetCount="20"
        CompletionListCssClass="autocomplete_completionListElement"
        CompletionListItemCssClass="autocomplete_listItem"
        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
        DelimiterCharacters=";, :"
        OnClientHiding="OnClientCompleted"
        OnClientPopulated="OnClientCompleted"
        OnClientPopulating="OnClientPopulating">

        <Animations>
        <OnShow>
            <Sequence>
                <%--Make the completion list transparent and then show it --%>
                <OpacityAction Opacity="0" />
                <HideAction Visible="true" />
                
                <%--Cache the original size of the completion list the first time
                    the animation is played and then set it to zero --%>
                <ScriptAction Script="
                    // Cache the size and setup the initial size
                    var behavior = $find('AutoCompleteBehaviorIDPlaceholder');
                    if (!behavior._height) {
                        var target = behavior.get_completionList();
                        behavior._height = target.offsetHeight - 2;
                        target.style.height = '0px';
                    }" />
                
                <%--Expand from 0px to the appropriate size while fading in--%>
                <Parallel Duration=".4">
                    <FadeIn />
                    <Length PropertyKey="height" StartValue="0" EndValueScript="$find('AutoCompleteBehaviorIDPlaceholder')._height" />
                </Parallel>
            </Sequence>
        </OnShow>
        <OnHide>
            <%--Collapse down to 0px and fade out--%>
            <Parallel Duration=".4">
                <FadeOut />
                <Length PropertyKey="height" StartValueScript="$find('AutoCompleteBehaviorIDPlaceholder')._height" EndValue="0" />
            </Parallel>
        </OnHide>
        </Animations>
    </ajaxToolkit:AutoCompleteExtender>

    <script type="text/javascript">
        // Work around browser behavior of "auto-submitting" simple forms
        var frm = document.getElementById("aspnetForm");
        if (frm) {
            frm.onsubmit = function () { return false; };
        }
    </script>
    <%-- Prevent enter in textbox from causing the collapsible panel from operating --%>
    <input type="submit" style="display: none;" />
</div>
