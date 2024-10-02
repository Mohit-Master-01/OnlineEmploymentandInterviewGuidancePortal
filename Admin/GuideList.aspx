<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="GuideList.aspx.cs" Inherits="OnlineJobPortal.Admin.GuideList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div style="background-image: url('../Images/bg.jpg'); width: 100%; height: 720px; background-repeat: no-repeat; background-size: cover; background-attachment: fixed">
        <div class="container-fluid pt-4 pb-4">
            <%-- <div>
             <asp:Label ID="lblMsg" runat="server"></asp:Label>
         </div>--%>

            <div class="btn-toolbar justify-content-between mb-3">
                <div class="btn-group">
                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                </div>

                <div class="input-group h-25">
                    <asp:HyperLink ID="Linkback" runat="server" NavigateUrl="~/Admin/ViewResume.aspx" CssClass="btn btn-secondary" Visible="false"> < Back </asp:HyperLink>
                </div>
            </div>



            <h3 class="text-center">Interview Guidance List</h3>

            <div class="row mb-3 pt-sm-3">
                <div class="col-md-12">
                    <asp:GridView ID="dtgGuideList" runat="server" CssClass="table table-hover table-bordered"
                        EmptyDataText="No record to display..!" AutoGenerateColumns="False" AllowPaging="True" PageSize="5"
                        OnPageIndexChanging="dtgGuideList_PageIndexChanging" DataKeyNames="g_id" OnRowDeleting="dtgGuideList_RowDeleting" OnRowDataBound="dtgGuideList_RowDataBound">
                        <Columns>

                            <asp:BoundField DataField="Sr.No" HeaderText="Sr.No">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>

                            <asp:BoundField DataField="Title" HeaderText="Title">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>

                            <asp:BoundField DataField="Category" HeaderText="Category">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>

                            <asp:BoundField DataField="Level" HeaderText="Levels">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>

                            <%--<asp:BoundField DataField="guide" HeaderText="guide">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>--%>

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
