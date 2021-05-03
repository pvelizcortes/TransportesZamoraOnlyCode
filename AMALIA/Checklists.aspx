<%@ Page Title="" Language="C#" EnableEventValidation="false" MasterPageFile="~/Amalia.Master" AutoEventWireup="true" CodeBehind="Checklists.aspx.cs" Inherits="AMALIA.Checklists" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BODY" runat="server">

    <style>
        .cell-letra-chica {
            text-align: left !important;
            font-size: 11px !important;
        }
    </style>
    <section class="content">
        <div class="container">
            <div class="row clearfix">
                <div class="col-lg-12">
                    <div class="card">
                        <div class="body block-header">
                            <div class="row">
                                <div class="col-sm-12">
                                    <span style="font-size: large"><strong>CHECKLISTS <i class="fa fa-file-pdf"></i></strong></span>
                                    <ul class="breadcrumb p-l-0 p-b-0 float-right">
                                        <li class="breadcrumb-item"><a href="index.aspx"><i class="icon-home"></i></a></li>
                                        <li class="breadcrumb-item"><a href="index.aspx">Index</a></li>
                                        <li class="breadcrumb-item active">Checklists</li>
                                    </ul>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12 col-md-12 col-sm-12">
                                    <asp:UpdatePanel runat="server" ID="UP_PRINCIPAL">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="G_PRINCIPAL" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <!-- LISTADO -->
                                            <asp:Panel runat="server" ID="PANEL_PRINCIPAL">
                                                <div class="row clearfix">
                                                    <div class="col-sm-2">
                                                        FECHA DESDE:
                                                        <br />
                                                        <asp:TextBox runat="server" ID="FILTRA_FECHA_DESDE" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        FECHA HASTA:
                                                        <br />
                                                        <asp:TextBox runat="server" ID="FILTRA_FECHA_HASTA" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        ESTADO:
                                                        <br />
                                                        <asp:DropDownList runat="server" ID="CB_ESTADOS" CssClass="form-control">
                                                            <asp:ListItem Text="-- TODOS --" Value="-1" Selected="True"></asp:ListItem>
                                                            <asp:ListItem Text="NUEVO" Value="NUEVO"></asp:ListItem>
                                                            <asp:ListItem Text="APROBADO" Value="APROBADO"></asp:ListItem>
                                                            <asp:ListItem Text="RECHAZADO" Value="RECHAZADO"></asp:ListItem>
                                                            <asp:ListItem Text="EDITADO" Value="EDITADO"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="row clearfix">
                                                    <div class="col-sm-2">
                                                        <br />
                                                        <asp:LinkButton runat="server" ID="B_FILTRAR" ClientIDMode="AutoID" OnClick="B_FILTRAR_Click" CssClass="btn btn-primary btn-sm btn-round"><i class="fa fa-search"></i> FILTRAR</asp:LinkButton>
                                                    </div>
                                                </div>
                                                <hr />
                                                <div class="row">
                                                    <div class="col-sm-12 table-responsive">
                                                        <asp:GridView DataKeyNames="id_checklist_completado, id_checklist, estado, fecha_aprobacion" ClientIDMode="AutoID" runat="server" ID="G_PRINCIPAL"
                                                            CssClass="table table-bordered tablaprincipal table-hover js-exportable table-condensed" Font-Size="12px" OnRowCommand="G_PRINCIPAL_RowCommand"
                                                            AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" OnRowDataBound="G_PRINCIPAL_RowDataBound">
                                                            <HeaderStyle CssClass="thead-dark" />
                                                            <Columns>
                                                                <asp:BoundField DataField="id_checklist_completado" Visible="false" />
                                                                <asp:BoundField DataField="id_checklist" Visible="false" />
                                                                <asp:TemplateField HeaderText="Ver" ItemStyle-Font-Bold="true">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton runat="server" ToolTip="VER CHECKLIST" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="Editar" CssClass="text-purple">
                                                                                <i class="fa fa-edit fa-2x"></i>                                                                    
                                                                        </asp:LinkButton>
                                                                        &nbsp;      &nbsp;
                                                                          <asp:LinkButton ToolTip="Imprimir PDF" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="Print" OnClientClick='<%# String.Format("return ImprimeOC({0});", Eval("id_checklist_completado")) %>'>
                                                                            <i class="fa fa-file-pdf fa-2x"></i>
                                                                          </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="fecha" HeaderText="FECHA" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
                                                                <asp:BoundField DataField="nombre_inspeccion" HeaderText="INSPECCIONA" />
                                                                <asp:BoundField DataField="nombre_conductor" HeaderText="CONDUCTOR" />
                                                                <asp:BoundField DataField="patente_camion" HeaderText="PATENTE" ItemStyle-Font-Bold="true" />
                                                                <asp:BoundField DataField="nombre_proveedor" HeaderText="PROVEEDOR" />
                                                                <asp:BoundField DataField="observacion" HeaderText="OBSERVACION" />
                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Estado" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <div id="div_estado" runat="server"></div>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Estado" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <div id="div_fecha" runat="server"></div>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="usuario_aprobacion" HeaderText="COORDINADOR" />
                                                                <asp:BoundField DataField="observacion_aprobacion" HeaderText="OBS. COORDINADOR" />
                                                            </Columns>
                                                            <EmptyDataTemplate>
                                                                No se encontraron resultados.
                                                            </EmptyDataTemplate>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                            <!-- ENC -->
                                            <asp:Panel ID="PANEL_ENC" runat="server" Visible="false">
                                                <form class="form-horizontal">
                                                    <div style="display: none">
                                                        <asp:TextBox runat="server" ID="T_IDCHECKLIST"></asp:TextBox>
                                                        <asp:TextBox runat="server" ID="T_IDCHECKLISTCOMPLETADO"></asp:TextBox>
                                                    </div>

                                                    <h4>Encabezado</h4>
                                                    <div class="row">
                                                        <div class="col-sm-12 table-responsive">
                                                            <asp:GridView runat="server" ID="G_ENCABEZADO" AutoGenerateColumns="false" CssClass="table table-bordered tablaprincipal table-hover js-exportable table-condensed">
                                                                <HeaderStyle CssClass="thead-dark" />
                                                                <Columns>
                                                                    <asp:BoundField DataField="fecha" HeaderText="FECHA" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
                                                                    <asp:BoundField DataField="nombre_inspeccion" HeaderText="INSPECCIONA" />
                                                                    <asp:BoundField DataField="nombre_conductor" HeaderText="CONDUCTOR" />
                                                                    <asp:BoundField DataField="rut" HeaderText="RUT" />
                                                                    <asp:BoundField DataField="patente_camion" HeaderText="PATENTE" ItemStyle-Font-Bold="true" />
                                                                    <asp:BoundField DataField="patente_rampla" HeaderText="PATENTE RAMPLA" ItemStyle-Font-Bold="true" />
                                                                    <asp:BoundField DataField="nombre_proveedor" HeaderText="PROVEEDOR" />
                                                                    <asp:BoundField DataField="observacion" HeaderText="OBSERVACION" />
                                                                </Columns>
                                                                <EmptyDataTemplate>
                                                                    No se encontraron resultados.
                                                                </EmptyDataTemplate>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                    <h4>Respuestas</h4>
                                                    <div class="row">
                                                        <div class="col-sm-12 table-responsive">
                                                            <asp:GridView runat="server" ID="G_RESPUESTAS" AutoGenerateColumns="false" CssClass="table table-bordered tablaprincipal table-hover js-exportable table-condensed">
                                                                <HeaderStyle CssClass="thead-dark" />
                                                                <Columns>
                                                                    <asp:BoundField DataField="nombre_pregunta" HeaderText="PREGUNTA" />
                                                                    <asp:BoundField DataField="respuesta" HeaderText="RESPUESTAS" />
                                                                </Columns>
                                                                <EmptyDataTemplate>
                                                                    No se encontraron resultados.
                                                                </EmptyDataTemplate>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                    <h4>Imagenes</h4>
                                                    <div class="row">
                                                        <div class="col-sm-12">
                                                            <div id="imagenes_div" runat="server"></div>
                                                        </div>
                                                    </div>
                                                    <hr />
                                                    <h4>CAMBIAR <b>ESTADO</b></h4>
                                                    <div class="row">
                                                        <div class="col-sm-12">
                                                            <small>Escriba la observación y luego elija la opción. (observación opcional)</small>
                                                            <asp:TextBox runat="server" ID="t_observacion" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <hr />
                                                    <div class="row">
                                                        <div class="col-sm-12">
                                                            <asp:LinkButton ClientIDMode="AutoID" CssClass="btn btn-primary btn-sm" runat="server" ID="bVerPDF" OnClick="bVerPDF_Click" OnClientClick="ImprimeOC2();"><i class="fa fa-eye"></i>&nbsp;VER PDF</asp:LinkButton>
                                                            <asp:LinkButton ClientIDMode="AutoID" CssClass="btn btn-success btn-sm" runat="server" ID="bAprobar" OnClick="bAprobar_Click"><i class="fa fa-check"></i>&nbsp;APROBAR</asp:LinkButton>
                                                            <asp:LinkButton ClientIDMode="AutoID" CssClass="btn btn-danger btn-sm" runat="server" ID="bRechazar" OnClick="bRechazar_Click"><i class="fa fa-check"></i>&nbsp;RECHAZAR</asp:LinkButton>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-12 text-right">
                                                            <asp:LinkButton ClientIDMode="AutoID" CssClass="btn btn-raised btn-default btn-round waves-effect" runat="server" ID="B_VOLVER" OnClick="B_VOLVER_Click"><i class="fa fa-undo"></i>&nbsp;VOLVER</asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </form>
                                            </asp:Panel>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
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
            var table = $('#<%= G_PRINCIPAL.ClientID %>')[0].rows.length;
            if (table > 2) {
                $('#<%= G_PRINCIPAL.ClientID %>').DataTable({
                    destroy: true,
                    "pageLength": 50,
                    stateSave: true,
                    dom: 'lBfrtip',
                    buttons: [
                        'copy',
                        'print'
                    ],
                    "language": {
                        "url": "assets/Spanish.json"
                    },
                    columnDefs: [{
                        type: 'date-uk',
                        targets: [1]
                    }]
                });
            }
        }

        function ImprimeOC(id_checklist_completado) {
            var win = window.open('ImprimeChecklist.aspx?id_checklist_completado=' + id_checklist_completado, '_blank');
            win.focus();
        }

        function ImprimeOC2() {
            var id_checklist_completado = document.getElementById('<%= T_IDCHECKLISTCOMPLETADO.ClientID %>').value;
            var win = window.open('ImprimeChecklist.aspx?id_checklist_completado=' + id_checklist_completado, '_blank');
            win.focus();
        }
    </script>
</asp:Content>
