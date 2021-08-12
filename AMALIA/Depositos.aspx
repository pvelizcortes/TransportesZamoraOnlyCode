<%@ Page Title="" Language="C#" EnableEventValidation="false" MasterPageFile="~/Amalia.Master" AutoEventWireup="true" CodeBehind="Depositos.aspx.cs" Inherits="AMALIA.Depositos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BODY" runat="server">

    <style>
        .cell-letra-chica {
            text-align: left !important;
            font-size: 11px !important;
        }

        .combo-bold {
            font-weight: bold !important;
            color: black;
        }

        .combo-disabled {
            color: gray;
            font-style: italic;
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
                                    <ul class="breadcrumb p-l-0 p-b-0 float-right">
                                        <li class="breadcrumb-item"><a href="index.aspx"><i class="icon-home"></i></a></li>
                                        <li class="breadcrumb-item"><a href="index.aspx">Index</a></li>
                                        <li class="breadcrumb-item active">Depositos</li>
                                    </ul>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12 col-md-12 col-sm-12">
                                    <asp:UpdatePanel runat="server" ID="UP_PRINCIPAL">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="G_PRINCIPAL" />
                                            <asp:AsyncPostBackTrigger ControlID="B_NUEVO" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <!-- LISTADO -->
                                            <asp:Panel runat="server" ID="PANEL_PRINCIPAL">
                                                <div class="row clearfix">
                                                    <div class="col-sm-12">
                                                        <asp:LinkButton CssClass="btn btn-sm btn-primary" ClientIDMode="AutoID" runat="server" ID="B_NUEVO" OnClick="B_NUEVO_Click" Visible="false">
                                                                                <b><small>+ INGRESAR NUEVO DEPOSITO</small></b>
                                                        </asp:LinkButton>

                                                     <%--   <asp:LinkButton CssClass="btn btn-sm btn-primary" ClientIDMode="AutoID" runat="server" ID="LinkButton1" OnClick="LinkButton1_Click">
                                                                                <b><small>RECALCULAR</small></b>
                                                        </asp:LinkButton>--%>
                                                    </div>
                                                </div>
                                                <hr />
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
                                                        SOLICITANTE:
                                                       
                                                        <br />
                                                        <asp:DropDownList runat="server" ID="CB_USUARIOS" CssClass="form-control"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        CONDUCTOR:
                                                       
                                                        <br />
                                                        <asp:DropDownList runat="server" ID="CB_CONDUCTOR" CssClass="form-control"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="row clearfix">
                                                    <div class="col-sm-2">
                                                        TIPO:
                                                       
                                                        <br />
                                                        <asp:DropDownList runat="server" ID="CB_TIPO" CssClass="form-control">
                                                            <asp:ListItem Selected="True" Text="-- Seleccione --" Value="-1"></asp:ListItem>
                                                            <asp:ListItem Text="FONDO POR RENDIR" Value="FONDO POR RENDIR"></asp:ListItem>
                                                            <asp:ListItem Text="VIATICO" Value="VIATICO"></asp:ListItem>
                                                            <asp:ListItem Text="SALDO FONDO POR RENDIR" Value="SALDO FONDO POR RENDIR"></asp:ListItem>
                                                            <asp:ListItem Text="SALDO VIATICO" Value="SALDO VIATICO"></asp:ListItem>
                                                            <asp:ListItem Text="DESCUENTO FONDO POR RENDIR" Value="DESCUENTO FONDO POR RENDIR"></asp:ListItem>
                                                            <asp:ListItem Text="DESCUENTO VIATICO" Value="DESCUENTO VIATICO"></asp:ListItem>
                                                            <asp:ListItem Text="DEPOSITO" Value="DEPOSITO"></asp:ListItem>
                                                            <asp:ListItem Text="BONO" Value="BONO"></asp:ListItem>
                                                            <asp:ListItem Text="DOBLE CONDUCTOR" Value="DOBLE CONDUCTOR"></asp:ListItem>
                                                            <asp:ListItem Text="PRESTAMO" Value="PRESTAMO"></asp:ListItem>
                                                            <asp:ListItem Text="SALDO" Value="SALDO"></asp:ListItem>
                                                            <asp:ListItem Text="SOBRE" Value="SOBRE"></asp:ListItem>
                                                            <asp:ListItem Text="DESCUENTO" Value="DESCUENTO"></asp:ListItem>
                                                            <asp:ListItem Text="VARIOS" Value="VARIOS"></asp:ListItem>
                                                            <asp:ListItem Text="OTRO" Value="OTRO"></asp:ListItem>
                                                            <asp:ListItem Text="FONDO POR RENDIR" Value="FONDO POR RENDIR"></asp:ListItem>
                                                            <asp:ListItem Text="VIATICO" Value="BONO"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        ESTADO:
                                                       
                                                        <br />
                                                        <asp:DropDownList runat="server" ID="CB_ESTADO" CssClass="form-control">
                                                            <asp:ListItem Selected="True" Text="-- Seleccione --" Value="-1"></asp:ListItem>
                                                            <asp:ListItem Text="DEPOSITADO" Value="DEPOSITADO"></asp:ListItem>
                                                            <asp:ListItem Text="NO DEPOSITADO" Value="NO DEPOSITADO"></asp:ListItem>
                                                            <asp:ListItem Text="NULO" Value="NULO"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <br />
                                                        <asp:LinkButton runat="server" ID="B_FILTRAR" ClientIDMode="AutoID" OnClick="B_FILTRAR_Click" CssClass="btn btn-primary btn-sm btn-round"><i class="fa fa-search"></i> FILTRAR</asp:LinkButton>
                                                    </div>
                                                </div>
                                                <hr />
                                                <div class="row">
                                                    <div class="col-sm-12 table-responsive">
                                                        <asp:GridView DataKeyNames="idDeposito, id_detalle_deposito, estado, diferencia, num_viaje" ClientIDMode="AutoID" runat="server" ID="G_PRINCIPAL"
                                                            CssClass="table table-bordered tablaprincipal table-hover js-exportable" Font-Size="12px" OnRowCommand="G_PRINCIPAL_RowCommand"
                                                            AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" OnRowDataBound="G_PRINCIPAL_RowDataBound">
                                                            <HeaderStyle CssClass="thead-dark" />
                                                            <Columns>
                                                                <%--      <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="Editar">
                                                                            <i class="icon-pencil" style="font-size:20px"></i>
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>--%>
                                                                <asp:TemplateField HeaderText="OPERACIÓN" ItemStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton runat="server" ToolTip="Editar Deposito" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="Editar" CssClass="text-purple">
                                                                                 <b style="font-size:14px"><%# Eval("num_operacion") %> </b>                                                                          
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="id_detalle_deposito" Visible="false" />
                                                                <asp:BoundField DataField="idDeposito" Visible="false" />
                                                                <asp:BoundField DataField="num_viaje" HeaderText="VIAJE" />
                                                                <asp:BoundField DataField="fecha_viaje" HeaderText="FECHA" DataFormatString="{0:dd/MM/yyyy}" />
                                                                <asp:BoundField DataField="usuario" HeaderText="SOLICITANTE" />
                                                                <asp:BoundField DataField="nombre_conductor" HeaderText="CONDUCTOR" />
                                                                <asp:BoundField DataField="tipo" HeaderText="TIPO" />

                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Estado" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <div id="div_estado" runat="server"></div>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="comentario" HeaderText="COMENTARIO" />
                                                                <asp:BoundField DataField="valor" ItemStyle-HorizontalAlign="Right" ItemStyle-Font-Bold="true" HeaderText="VALOR" DataFormatString="{0:C0}" />
                                                                <asp:BoundField DataField="monto_depositado" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="text-purple" ItemStyle-Font-Bold="true" HeaderText="MONTO DEP." DataFormatString="{0:C0}" />
                                                                <asp:BoundField DataField="diferencia" ItemStyle-HorizontalAlign="Right" ItemStyle-Font-Bold="true" HeaderText="DIFF" DataFormatString="{0:C0}" />

                                                                <asp:BoundField DataField="usuario_Admin" HeaderText="ADMINISTRADOR" />
                                                                <asp:BoundField DataField="comentario_admin" HeaderText="COMENTARIO ADMIN." />
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
                                                        <asp:TextBox runat="server" ID="T_ID"></asp:TextBox>
                                                        <asp:TextBox runat="server" ID="T_ID_DETALLE"></asp:TextBox>
                                                    </div>
                                                    <div class="row clearfix">
                                                        <div class="col-sm-2">
                                                            <div class="form-group">
                                                                <small><b>Nº Operacion</b></small>
                                                                <asp:TextBox runat="server" ReadOnly="true" CssClass="form-control" ID="tNumOperacion"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-10">
                                                            <br />
                                                            <asp:LinkButton runat="server" ID="bAprobarTodas" OnClick="bAprobarTodas_Click" OnClientClick="return confirm('Esta seguro(a) de marcar todos los depositos como Depositados?');" CssClass="btn btn-sm btn-primary">Marcar todos como depositado &nbsp;<i class="fa fa-check"></i></asp:LinkButton>
                                                            <div class="btn btn-info btn-sm" onclick="ImprimeOC2();">Imprimir <i class="fa fa-print"></i></div>
                                                            <asp:LinkButton runat="server" ID="bBorrarDeposito" OnClick="bBorrarDeposito_Click" OnClientClick="return confirm('Esta seguro(a) de eliminar la operación y todas sus solicitudes de deposito?');" CssClass="btn btn-sm btn-danger" Visible="false">Eliminar operación y solicitudes &nbsp;<i class="fa fa-times"></i></asp:LinkButton>
                                                        </div>
                                                    </div>
                                                    <div class="row  clearfix">
                                                        <div class="col-sm-12">
                                                            <div class="table-responsive">
                                                                <h6>Depositos:</h6>
                                                                <asp:GridView DataKeyNames="id_detalle_deposito, id_deposito, estado, diferencia, num_viaje" ClientIDMode="AutoID" runat="server"
                                                                    ID="gDetalle" CssClass="table tablaprincipal table-bordered table-condensed table-sm"
                                                                    OnRowCommand="gDetalle_RowCommand" OnRowDataBound="gDetalle_RowDataBound" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true"
                                                                    Font-Size="Small">
                                                                    <HeaderStyle CssClass="thead-dark" />
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ToolTip="Editar Detalle" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="Editar">
                                                                                <i class="fa fa-edit text-purple fa-2x"></i>
                                                                                </asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField DataField="id_detalle_deposito" Visible="false" />
                                                                        <asp:BoundField DataField="id_deposito" Visible="false" />
                                                                        <asp:BoundField DataField="num_viaje" HeaderText="VIAJE" />
                                                                        <asp:BoundField DataField="fecha_viaje" HeaderText="FECHA" DataFormatString="{0:dd/MM/yyyy}" />
                                                                        <asp:BoundField DataField="usuario" HeaderText="SOLICITANTE" />
                                                                        <asp:BoundField DataField="nombre_conductor" HeaderText="CONDUCTOR" />
                                                                        <asp:BoundField DataField="tipo" HeaderText="TIPO" />
                                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Estado" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <div id="div_estado" runat="server"></div>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField DataField="comentario" HeaderText="COMENTARIO" />
                                                                        <asp:BoundField DataField="valor" ItemStyle-HorizontalAlign="Right" ItemStyle-Font-Bold="true" HeaderText="VALOR" DataFormatString="{0:C0}" />
                                                                        <asp:BoundField DataField="monto_depositado" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="text-purple" ItemStyle-Font-Bold="true" HeaderText="MONTO DEP." DataFormatString="{0:C0}" />
                                                                        <asp:BoundField DataField="diferencia" ItemStyle-HorizontalAlign="Right" ItemStyle-Font-Bold="true" HeaderText="DIFF" DataFormatString="{0:C0}" />

                                                                        <asp:BoundField DataField="usuario_Admin" HeaderText="ADMINISTRADOR" />
                                                                        <asp:BoundField DataField="comentario_admin" HeaderText="COMENTARIO ADMIN." />
                                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ToolTip="Eliminar Detalle" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="Borrar" OnClientClick="return confirm('Desea eliminar el registro seleccionado?');">
                                                                                <i class="fa fa-trash text-purple fa-2x"></i>
                                                                                </asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <EmptyDataTemplate>
                                                                        No se encontraron resultados.
                                                                   
                                                                    </EmptyDataTemplate>
                                                                </asp:GridView>
                                                                <asp:LinkButton runat="server" ID="bNuevoDetalle" OnClick="bNuevoDetalle_Click" CssClass="btn btn-sm btn-primary">Agregar Nuevo Deposito <i class="fa fa-plus"></i></asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div id="divDetalle" runat="server" visible="false">
                                                        <hr />
                                                        <asp:Label runat="server" ID="lblDetalle" Text="Agregando nuevo deposito:" Font-Size="Large" Font-Bold="true" CssClass="text-purple"></asp:Label>
                                                        <br />
                                                        <br />
                                                        <div class="row clearfix">
                                                            <div class="col-sm-3">
                                                                <div class="form-group">
                                                                    <small><b>Nº Viaje</b></small>
                                                                    <asp:TextBox runat="server" CssClass="form-control" ID="tNumViaje"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-3">
                                                                <div class="form-group">
                                                                    <small><b>Fecha</b></small>
                                                                    <asp:TextBox runat="server" TextMode="Date" CssClass="form-control" ID="tFecha"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-3">
                                                                <div class="form-group">
                                                                    <small><b>Conductor</b></small>
                                                                    <asp:DropDownList runat="server" ID="cbConductor" CssClass="form-control"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-3">
                                                                <div class="form-group">
                                                                    <small><b>Tipo</b></small>
                                                                    <asp:DropDownList runat="server" ID="cbTipo" CssClass="form-control">
                                                                        <asp:ListItem Text="FONDO POR RENDIR" Value="FONDO POR RENDIR" Selected="True" class="combo-bold"></asp:ListItem>
                                                                        <asp:ListItem Text="VIATICO" Value="VIATICO" class="combo-bold"></asp:ListItem>
                                                                        <asp:ListItem Text="SALDO FONDO POR RENDIR" Value="SALDO FONDO POR RENDIR" class="combo-bold"></asp:ListItem>
                                                                        <asp:ListItem Text="SALDO VIATICO" Value="SALDO VIATICO" class="combo-bold"></asp:ListItem>
                                                                        <asp:ListItem Text="DESCUENTO FONDO POR RENDIR" Value="DESCUENTO FONDO POR RENDIR" class="combo-bold"></asp:ListItem>
                                                                        <asp:ListItem Text="DESCUENTO VIATICO" Value="DESCUENTO VIATICO" class="combo-bold"></asp:ListItem>
                                                                        <asp:ListItem Text="-- ANTIGUOS --" Value="-1" class="combo-disabled" disabled="disabled"></asp:ListItem>
                                                                        <asp:ListItem Text="SALDO" Value="SALDO" class="combo-bold"></asp:ListItem>
                                                                        <asp:ListItem Text="DESCUENTO" Value="DESCUENTO" class="combo-bold"></asp:ListItem>
                                                                        <asp:ListItem Text="-- NO DISPONIBLES --" Value="-1" class="combo-disabled" disabled="disabled"></asp:ListItem>
                                                                        <asp:ListItem Text="DEPOSITO" Value="DEPOSITO" class="combo-disabled" disabled="disabled"></asp:ListItem>
                                                                        <asp:ListItem Text="BONO" Value="BONO" class="combo-disabled" disabled="disabled"></asp:ListItem>
                                                                        <asp:ListItem Text="DOBLE CONDUCTOR" Value="DOBLE CONDUCTOR" class="combo-disabled" disabled="disabled"></asp:ListItem>
                                                                        <asp:ListItem Text="PRESTAMO" Value="PRESTAMO" class="combo-disabled" disabled="disabled"></asp:ListItem>

                                                                        <asp:ListItem Text="SOBRE" Value="SOBRE" class="combo-disabled" disabled="disabled"></asp:ListItem>

                                                                        <asp:ListItem Text="VARIOS" Value="VARIOS" class="combo-disabled" disabled="disabled"></asp:ListItem>
                                                                        <asp:ListItem Text="OTRO" Value="OTRO" class="combo-disabled" disabled="disabled"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row clearfix">
                                                            <div class="col-sm-3">
                                                                <div class="form-group">
                                                                    <small><b>Valor ($)</b></small>
                                                                    <asp:TextBox runat="server" CssClass="form-control" TextMode="Number" ID="tValor"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-3">
                                                                <div class="form-group">
                                                                    <small><b>Comentario</b></small>
                                                                    <asp:TextBox runat="server" CssClass="form-control" ID="tComentario"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div runat="server" id="divAdmin" visible="false">
                                                            <hr />
                                                            <asp:Label runat="server" ID="Label1" Text="Opciones Administrador:" Font-Size="Large" Font-Bold="true" CssClass="text-purple"></asp:Label>
                                                            <div class="row clearfix">
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <small><b>Comentario Administrador</b></small>
                                                                        <asp:TextBox runat="server" CssClass="form-control" ID="tComentarioAdmin"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <small><b>Monto Depositado</b></small>
                                                                        <asp:TextBox runat="server" CssClass="form-control" ID="tMontoDepositadoAdmin"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <small><b>Estado</b></small>
                                                                        <asp:DropDownList runat="server" ID="cbEstadoAdmin" CssClass="form-control">
                                                                            <asp:ListItem Text="NO DEPOSITADO" Value="NO DEPOSITADO" Selected="True"></asp:ListItem>
                                                                            <asp:ListItem Text="DEPOSITADO" Value="DEPOSITADO"></asp:ListItem>
                                                                            <asp:ListItem Text="DESCONTADO" Value="DESCONTADO"></asp:ListItem>
                                                                            <asp:ListItem Text="NULO" Value="NULO"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-3">
                                                                <br />
                                                                <asp:LinkButton runat="server" ID="bAgregarDetalle" Visible="false" OnClick="bAgregarDetalle_Click" CssClass="btn btn-block  btn-primary">GUARDAR <i class="fa fa-save"></i></asp:LinkButton>
                                                                <small><i class="fa fa-info"></i>(Recordar que la GT debe existir en el sistema antes de agregar depositos).</small>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-12 text-right">
                                                            <hr />
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
            var table = $('#<%= G_PRINCIPAL.ClientID %>');
            if (table[0] != null && table[0] != undefined) {
                if (table[0].rows.length > 1) {
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
                            targets: [3]
                        }]
                    });
                }
            }
        }

        function ImprimeOC2() {
            var id_deposito = document.getElementById('<%= T_ID.ClientID %>').value;
            var win = window.open('Imprime_deposito.aspx?id_deposito=' + id_deposito, '_blank');
            win.focus();
        }
    </script>
</asp:Content>
