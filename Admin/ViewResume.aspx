<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="ViewResume.aspx.cs" Inherits="OnlineJobPortal.Admin.ViewResume" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div style="background-image: url('../Images/bg.jpg'); width: 100%; height: 720px; background-repeat: no-repeat; background-size: cover; background-attachment: fixed">
        <div class="container-fluid pt-4 pb-4">
            <div>
                <asp:Label ID="lblMsg" runat="server"></asp:Label>
            </div>

            <h3 class="text-center">View Resume/Download Resume</h3>

            <div class="row mb-3 pt-sm-3">
                <div class="col-md-12">
                    <asp:GridView ID="dtgResume" runat="server" CssClass="table table-hover table-bordered" EmptyDataText="No record to display..!" AutoGenerateColumns="False" AllowPaging="True" PageSize="10" OnPageIndexChanging="dtgResume_PageIndexChanging1" DataKeyNames="AJ_id" OnRowDeleting="dtgResume_RowDeleting" OnRowDataBound="dtgResume_RowDataBound" OnSelectedIndexChanged="dtgResume_SelectedIndexChanged">
                        <Columns>

                            <asp:BoundField DataField="Sr.No" HeaderText="Sr.No">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>

                            <asp:BoundField DataField="CompanyName" HeaderText="Company Name">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>

                            <asp:BoundField DataField="Title" HeaderText="Job Title">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>

                            <asp:BoundField DataField="Name" HeaderText="User Name">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>

                            <asp:BoundField DataField="Email" HeaderText="User Email">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>

                            <asp:BoundField DataField="Mobile" HeaderText="Mobile No.">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                                                        
                            <asp:TemplateField HeaderText="Resume">
                                <ItemTemplate>
                                    
                                    <asp:Button ID="DownloadPDF" runat="server" Text="Download PDF" CommandArgument='<%# Eval("u_id") %>' OnClick="DownloadPDF_Click" />
<%--                                    <asp:HyperLink ID="hlDownload" runat="server" NavigateUrl='<%# ResolveUrl("~/Upload/" +Eval("Resume")) %>'><i class="fas fa-download"></i> Download</asp:HyperLink>--%>
                                    <asp:HiddenField ID="hdnJob_id" runat="server" Value='<%# Eval("Job_id") %>' Visible="false" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:CommandField CausesValidation="false" HeaderText="Delete" ShowDeleteButton="true" DeleteImageUrl="../assets/img/icon/TrashIcon.png" ButtonType="Image">
                                <ControlStyle Height="25px" Width="25px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:CommandField>

                        </Columns>
                        <HeaderStyle BackColor="#7200cf" ForeColor="White" />
                    </asp:GridView>
                </div>
            </div>

        </div>
    </div>

</asp:Content>
