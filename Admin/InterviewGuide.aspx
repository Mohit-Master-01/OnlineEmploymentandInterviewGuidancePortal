<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="InterviewGuide.aspx.cs" Inherits="OnlineJobPortal.Admin.InterviewGuide" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div style="background-image: url('../Images/bg.jpg'); width: 100%; height: 720px; background-repeat: no-repeat; background-size: cover; background-attachment: fixed">
        <div class="container pt-4 pb-4">
            <%--            <div>
                <asp:Label ID="lblMsg" runat="server"></asp:Label>
            </div>--%>
            <div class="btn-toolbar justify-content-between mb-3">
                <div class="btn-group">
                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                </div>
            </div>

            <h3 class="text-center">Interview Guidance</h3>

            <div class="row mr-lg-5 ml-lg-5 mb-3">
                <div class="col-md-6 pt-3">
                    <label for="txtGuideTitle" style="font-weight: 600">Title</label>
                    <asp:TextBox ID="txtGuideTitle" runat="server" CssClass="form-control" placeholder="Ex. Php, JavaScript" required></asp:TextBox>
                </div>
                <div class="col-md-6 pt-3">
                    <label for="txtDescription" style="font-weight: 600">Description</label>
                    <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" placeholder="Enter Description of language" required></asp:TextBox>
                </div>
            </div>

            <div class="row mr-lg-5 ml-lg-5 mb-3">
                <div class="col-md-6 pt-3">
                    <label for="txtCategory" style="font-weight: 600">Qualification/Education Required</label>
                    <asp:DropDownList ID="ddlType" runat="server" AutoPostBack="true" CssClass="form-control w-100" AppendDataBoundItems="true" DataTextField="Category" DataValueField="g_id" required>
                        <asp:ListItem Value="0">Type</asp:ListItem>
                        <asp:ListItem>Placement</asp:ListItem>
                        <asp:ListItem>Internship</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-6 pt-3">
                    <label for="txtLevel" style="font-weight: 600">Level</label>
                    <asp:DropDownList ID="ddlLevel" runat="server" AutoPostBack="true" CssClass="form-control w-100" AppendDataBoundItems="true" DataTextField="Level" DataValueField="g_id" required>
                        <asp:ListItem Value="0">Select</asp:ListItem>
                        <asp:ListItem>Basic</asp:ListItem>
                        <asp:ListItem>Moderate</asp:ListItem>
                        <asp:ListItem>Advance</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>

            <div class="row mr-lg-5 ml-lg-5 mb-3">
                <div class="col-md-6 pt-3">
                    <label for="txtGuide" style="font-weight: 600">Guide</label>
                    <asp:FileUpload ID="fuGuide" runat="server" CssClass="form-control pt-2" ToolTip=".doc, .docx, .pdf extension only" />
                </div>
            </div>

            <div class="row mr-lg-5 ml-lg-5 mb-3 pt-4">
                <div class="col-md-3 col-md-offset-2 mb-3">
                    <asp:Button ID="btnAdd" runat="server" Text="Add Guide" CssClass="btn btn-primary btn-block" BackColor="#7200cf" OnClick="btnAdd_Click" />
                </div>
            </div>

        </div>

    </div>

</asp:Content>
