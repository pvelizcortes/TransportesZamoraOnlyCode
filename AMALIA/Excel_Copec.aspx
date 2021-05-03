<%@ Page Title="" Language="C#" MasterPageFile="~/Amalia.Master" AutoEventWireup="true" CodeBehind="Excel_Copec.aspx.cs" Inherits="AMALIA.Excel_Copec" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BODY" runat="server">
      
    <section class="content">
        <div class="container">
            <div runat="server" id="DIV_DASH">
                <div class="row clearfix">
                    <div class="col-lg-12">
                        <div class="card">
                            <div class="body block-header">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <h2>Excel <b>Copec</b></h2>
                                        <hr />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <asp:UpdatePanel runat="server">
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="B_CARGAR_EXCEL" />
                                            </Triggers>
                                            <ContentTemplate>
                                                <h6><b>CARGAR ARCHIVO EXCEL DE COPEC: </b></h6>
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td style="width: 100%">
                                                            <asp:FileUpload runat="server" ID="SUBIR_EXCEL" />
                                                        </td>
                                                        <td>
                                                            <asp:Button runat="server" ID="B_CARGAR_EXCEL" OnClick="B_CARGAR_EXCEL_Click1" OnClientClick="relojito(true);" class="btn btn-success" Text="CARGAR" />
                                                        </td>
                                                    </tr>
                                                </table>
                                                <br />
                                                <small>Aqui se muestran los errores al importar el excel.</small>
                                                <asp:TextBox runat="server" ID="T_ERRORES" CssClass="form-control"></asp:TextBox>
                                                <hr />
                                                <div class="body table-responsive">
                                                    <div runat="server" id="AQUI_HTML"></div>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CODIGO_JAVASCRIPT" runat="server">
    <script>
        $(document).ready(function () {
            Datatables();
        });

        function Datatables() {
            try {
                $('#tablitapro').DataTable({
                    destroy: true,
                    stateSave: true,
                    dom: 'lBfrtip',
                    buttons: [
                        'copy', 'print'
                    ],
                    "language": {
                        "url": "assets/Spanish.json"
                    }
                });
            }
            catch (x) {

            }

        }

    </script>
</asp:Content>
