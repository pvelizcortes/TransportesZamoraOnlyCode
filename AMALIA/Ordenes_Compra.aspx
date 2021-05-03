<%@ Page Title="" Language="C#" EnableEventValidation="false" MasterPageFile="~/Amalia.Master" AutoEventWireup="true" CodeBehind="Ordenes_Compra.aspx.cs" Inherits="AMALIA.Ordenes_Compra" %>

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
                                    <ul class="breadcrumb p-l-0 p-b-0 float-right">
                                        <li class="breadcrumb-item"><a href="index.aspx"><i class="icon-home"></i></a></li>
                                        <li class="breadcrumb-item"><a href="index.aspx">Index</a></li>
                                        <li class="breadcrumb-item active">Ordenes de Compra</li>
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
                                                        <asp:LinkButton CssClass="btn btn-sm btn-primary" ClientIDMode="AutoID" runat="server" ID="B_NUEVO" OnClick="B_NUEVO_Click">
                                                                                <b><small>+ INGRESAR NUEVA ORDEN DE COMPRA</small></b>
                                                        </asp:LinkButton>
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
                                                        OC DESDE:
                                                        <br />
                                                        <asp:TextBox runat="server" ID="FILTRA_GT_DESDE" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        OC HASTA:
                                                        <br />
                                                        <asp:TextBox runat="server" ID="FILTRA_GT_HASTA" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        SOLICITANTE:
                                                        <br />
                                                        <asp:DropDownList runat="server" ID="CB_USUARIOS" CssClass="form-control"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        Nº FACTURA
                                                        <br />
                                                        <asp:TextBox runat="server" ID="FILTRA_FACTURA" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row clearfix">
                                                    <%-- <div class="col-sm-2">
                                                        AUTORIZADA:
                                                        <br />
                                                        <asp:DropDownList runat="server" ID="CB_FILTRO_AUT_MZ" CssClass="form-control">
                                                            <asp:ListItem Value="-1" Text="-- SELECCIONE --" Selected="True"></asp:ListItem>
                                                            <asp:ListItem Value="SI" Text="SI"></asp:ListItem>
                                                            <asp:ListItem Value="NO" Text="NO"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>--%>
                                                    <div class="col-sm-2">
                                                        AUTORIZADA:
                                                        <br />
                                                        <asp:DropDownList runat="server" ID="CB_FILTRO_AUT_FZ" CssClass="form-control">
                                                            <asp:ListItem Value="-1" Text="-- SELECCIONE --" Selected="True"></asp:ListItem>
                                                            <asp:ListItem Value="SI" Text="SI"></asp:ListItem>
                                                            <asp:ListItem Value="NO" Text="NO"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        CANCELADA
                                                        <br />
                                                        <asp:DropDownList runat="server" ID="CB_FILTRO_CANCELADA" CssClass="form-control">
                                                            <asp:ListItem Value="-1" Text="-- SELECCIONE --" Selected="True"></asp:ListItem>
                                                            <asp:ListItem Text="NO" Value="NO"></asp:ListItem>
                                                            <asp:ListItem Text="ABONADA" Value="ABONADA"></asp:ListItem>
                                                            <asp:ListItem Text="SI" Value="SI"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        FACTURADA
                                                        <br />
                                                        <asp:DropDownList runat="server" ID="CB_FILTRO_FACTURADA" CssClass="form-control">
                                                            <asp:ListItem Value="-1" Text="-- SELECCIONE --" Selected="True"></asp:ListItem>
                                                            <asp:ListItem Value="SI" Text="SI"></asp:ListItem>
                                                            <asp:ListItem Value="NO" Text="NO"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        SIS. FACT
                                                        <br />
                                                        <asp:DropDownList runat="server" ID="CB_FILTRO_SIS_FACT" CssClass="form-control">
                                                            <asp:ListItem Value="-1" Text="-- SELECCIONE --" Selected="True"></asp:ListItem>
                                                            <asp:ListItem Value="SI" Text="SI"></asp:ListItem>
                                                            <asp:ListItem Value="NO" Text="NO"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        ESTADO
                                                        <br />
                                                        <asp:DropDownList runat="server" ID="CB_FILTRO_ESTADO_FACT" CssClass="form-control"></asp:DropDownList>
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
                                                        <asp:GridView DataKeyNames="id_oc, id_oc_det, correlativo_oc" ClientIDMode="AutoID" runat="server" ID="G_PRINCIPAL"
                                                            CssClass="table table-bordered tablaprincipal table-hover js-exportable table-condensed" Font-Size="12px" OnRowCommand="G_PRINCIPAL_RowCommand"
                                                            AutoGenerateColumns="false" ShowHeaderWhenEmpty="true">
                                                            <HeaderStyle CssClass="thead-dark" />
                                                            <Columns>
                                                                <asp:BoundField DataField="id_oc" Visible="false" />
                                                                <asp:BoundField DataField="id_oc_det" Visible="false" />
                                                                <asp:BoundField DataField="id_proveedor" Visible="false" />
                                                                <asp:TemplateField HeaderText="Nº OC" ItemStyle-Font-Bold="true">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton runat="server" ToolTip="Editar OC" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="Editar" CssClass="text-purple">
                                                                                 <b><%# Eval("correlativo_oc") %> </b>                                                                          
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="fecha_oc" HeaderText="FECHA OC" DataFormatString="{0:dd/MM/yyyy}" />
                                                                <asp:BoundField DataField="solicitante" HeaderText="SOLICITANTE" />
                                                                <asp:TemplateField HeaderText="GLOSA" ItemStyle-Font-Bold="true">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton runat="server" ToolTip="Editar Glosa" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="EditarGlosa" CssClass="text-purple">
                                                                                 <b><%# Eval("GLOSA") %> </b>                                                                          
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="rut" HeaderText="RUT PROV." ItemStyle-Font-Bold="true" />
                                                                <asp:BoundField DataField="razon_social" HeaderText="RAZÓN SOCIAL" />
                                                                <asp:BoundField DataField="direccion" HeaderText="DIRECCIÓN" />
                                                                <asp:BoundField DataField="comuna" HeaderText="COMUNA" />
                                                                <asp:BoundField DataField="ciudad" HeaderText="CIUDAD" />
                                                                <%--           <asp:BoundField DataField="aprobado_mz" HeaderText="AUT M. ZAPATA" />--%>
                                                                <asp:BoundField DataField="aprobado_fz" HeaderText="AUTORIZADA" />
                                                                <asp:BoundField DataField="CANCELADA" HeaderText="CANCELADA" />
                                                                <asp:BoundField DataField="FACTURADA" HeaderText="FACTURADA" />
                                                                <asp:BoundField DataField="NUM_FACTURA" HeaderText="Nº FACTURA" />
                                                                <asp:BoundField DataField="SISTFACT" HeaderText="SIST. FACT" />
                                                                <asp:BoundField DataField="ESTADO" HeaderText="ESTADO" />

                                                                <asp:BoundField HeaderText="NETO" DataField="NETO" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C0}" ItemStyle-Font-Bold="true" />
                                                                <asp:BoundField HeaderText="IVA" DataField="IVA" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C0}" ItemStyle-Font-Bold="true" />
                                                                <asp:BoundField HeaderText="TOTAL" DataField="TOTAL" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C0}" ItemStyle-Font-Bold="true" />

                                                                <asp:BoundField DataField="observacion_enc" HeaderText="OBSERVACION OC" />
                                                                <asp:BoundField DataField="observacion" HeaderText="OBSERVACION GLOSA" />
                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText=" " ItemStyle-Font-Size="Large" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ToolTip="Imprimir PDF" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="Print" OnClientClick='<%# String.Format("return ImprimeOC({0});", Eval("id_oc")) %>'>
                                                                   <i class="fa fa-print"></i>
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
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
                                                <h4>
                                                    <b>
                                                        <i class="fa fa-file-invoice"></i>&nbsp;<asp:Label runat="server" ID="LBL_ESTADO" Text="Orden de Compra Nº : "></asp:Label>
                                                    </b>&nbsp;&nbsp;&nbsp;
                                                    <div class="btn btn-primary  btn-sm" onclick="ImprimeOC2();"><i class="fa fa-print"></i>Imprimir OC</div>
                                                    &nbsp;&nbsp;&nbsp;<asp:LinkButton ClientIDMode="AutoID" CssClass="btn btn-danger btn-sm" runat="server" ID="B_ELIMINAR_OC" OnClientClick="return confirm('Desea eliminar el registro seleccionado?');" OnClick="B_ELIMINAR_OC_Click"><i class="fa fa-times"></i>&nbsp;ELIMINAR OC</asp:LinkButton>
                                                </h4>
                                                <div runat="server" class="text-center" id="DIVAUTORIZADAS" visible="false">
                                                    <hr />
                                                    <div class="row clearfix  text-center">
                                                        <%-- <div class="col-sm-4">
                                                            <small><b>AUTORIZACION <span class="text-purple">MAURICIO ZAPATA</span></b></small><br />
                                                            <asp:Label runat="server" Font-Size="Large" Font-Bold="true" ID="LBLAUTMZ"></asp:Label>
                                                        </div>--%>
                                                        <div class="col-sm-6">
                                                            <small><b>AUTORIZADA FRANCISCA ESTAY</b></small><br />
                                                            <asp:Label runat="server" Font-Size="Large" Font-Bold="true" ID="LBLAUTFZ"></asp:Label>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <small><b>OBSERVACION</b></small><br />
                                                            <asp:Label runat="server" ID="LBLOBSERVACION"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <hr />
                                                    <asp:TextBox runat="server" CssClass="text-center" Style="width: 50%" Font-Size="Smaller" ID="T_COPYCLIPBOARD"></asp:TextBox>
                                                    <br />
                                                    <small class="text-primary">(Link de autorización)</small>
                                                </div>
                                                <hr />
                                                <form class="form-horizontal">
                                                    <div style="display: none">
                                                        <asp:TextBox runat="server" ID="T_ID"></asp:TextBox>
                                                    </div>
                                                    <div class="row clearfix">
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <small><b>Proveedor *</b> | <a href="PROVEEDORES_OC.aspx" target="_blank">+ Crear nuevo</a></small>
                                                                <asp:DropDownList runat="server" ID="CB_PROVEEDOR" CssClass="form-control combopro"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row clearfix">
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <small><b>Fecha emisión</b></small>
                                                                <asp:TextBox runat="server" TextMode="Date" CssClass="form-control" ID="T_FECHA_EMISION"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <small><b>Plazo entrega</b></small>
                                                                <asp:TextBox runat="server" TextMode="Date" CssClass="form-control" ID="T_PLAZOENTREGA"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row clearfix">
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <small><b>Solicitante</b></small>
                                                                <asp:TextBox runat="server" ID="T_SOLICITANTE" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <small><b>Destino</b></small>
                                                                <asp:TextBox runat="server" ID="T_DESTINO" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row clearfix">
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <small><b>Contacto</b></small>
                                                                <asp:TextBox runat="server" ID="T_CONTACTO" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <small><b>Email</b></small>
                                                                <asp:TextBox runat="server" ID="T_EMAIL" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row clearfix">
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <small><b>Observación</b></small>
                                                                <asp:TextBox runat="server" ID="T_OBSERVACION" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-4" style="display: none">
                                                            <div class="form-group">
                                                                <small><b>Clase</b></small>
                                                                <asp:TextBox runat="server" ID="T_CLASE" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div runat="server" id="DIV_DETALLE" visible="false">
                                                        <h6 class="text-purple"><b><i class="fa fa-list"></i></b>Agregar Detalle:</h6>
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <form class="form-horizontal">
                                                                    <div class="row clearfix">
                                                                        <div class="col-sm-1 nomargin">
                                                                            <div class="form-group">
                                                                                <small><b>LI</b></small>
                                                                                <asp:TextBox runat="server" CssClass="tz-input2" ID="TD_LI" placeholder=""></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-1 nomargin">
                                                                            <div class="form-group">
                                                                                <small><b>CANT.</b></small>
                                                                                <asp:TextBox runat="server" TextMode="Number" CssClass="tz-input2" ID="TD_CANT" placeholder=""></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-1 nomargin">
                                                                            <div class="form-group">
                                                                                <small><b>UM</b></small>
                                                                                <asp:TextBox runat="server" CssClass="tz-input2" ID="TD_UM" Text="C/U" placeholder=""></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-6 nomargin">
                                                                            <div class="form-group">
                                                                                <small><b>GLOSA</b></small>
                                                                                <asp:TextBox runat="server" CssClass="tz-input2" ID="TD_GLOSA" placeholder=""></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-2 nomargin">
                                                                            <div class="form-group">
                                                                                <small><b>$ UNIT.</b></small>
                                                                                <asp:TextBox runat="server" TextMode="Number" CssClass="tz-input2" ID="TD_PRECIOUNITARIO" placeholder=""></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-1 nomargin">
                                                                            <div class="form-group">
                                                                                <br />
                                                                                <asp:LinkButton runat="server" ID="B_AGREGAR_DETALLE" OnClick="B_AGREGAR_DETALLE_Click" CssClass="btn btn-sm btn-success"><i class="fa fa-plus"></i></asp:LinkButton>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </form>
                                                            </div>
                                                        </div>
                                                        <hr />
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <asp:GridView DataKeyNames="id_oc_det,id_oc" ClientIDMode="AutoID" runat="server"
                                                                    ID="G_DETALLE_DOCS" CssClass="table tablaprincipal table-bordered table-condensed table-sm"
                                                                    OnRowCommand="G_DETALLE_DOCS_RowCommand" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true"
                                                                    Font-Size="Small">
                                                                    <HeaderStyle CssClass="thead-dark" />
                                                                    <Columns>
                                                                        <asp:BoundField DataField="id_oc_det" Visible="false" />
                                                                        <asp:BoundField DataField="id_oc" Visible="false" />
                                                                        <asp:BoundField DataField="li" HeaderText="LI" />
                                                                        <asp:BoundField DataField="cant" HeaderText="CANT" />
                                                                        <asp:BoundField DataField="um" HeaderText="UM" />
                                                                        <asp:BoundField DataField="glosa" HeaderText="GLOSA" />
                                                                        <asp:BoundField DataField="unitario" HeaderText="$ UNIT." DataFormatString="{0:C0}" />
                                                                        <asp:BoundField DataField="neto" HeaderText="NETO" DataFormatString="{0:C0}" ItemStyle-Font-Bold="true" />
                                                                        <asp:BoundField DataField="iva" HeaderText="IVA" DataFormatString="{0:C0}" ItemStyle-Font-Bold="true" />
                                                                        <asp:BoundField DataField="total" HeaderText="TOTAL" DataFormatString="{0:C0}" ItemStyle-Font-Bold="true" />
                                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="Borrar" OnClientClick="return confirm('Desea eliminar el registro seleccionado?');">
                                                                                <i class="fa fa-times text-danger"></i>
                                                                                </asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <EmptyDataTemplate>
                                                                        No se encontraron resultados.
                                                                    </EmptyDataTemplate>
                                                                </asp:GridView>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-8">
                                                                &nbsp;
                                                            </div>
                                                            <div class="col-sm-4 text-right">
                                                                <table class="table tabl-condensed table-sm">
                                                                    <tr>
                                                                        <td><b>Neto:</b></td>
                                                                        <td>
                                                                            <asp:Label runat="server" ID="LB_NETO" Font-Bold="true"></asp:Label></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td><b>IVA:</b></td>
                                                                        <td>
                                                                            <asp:Label runat="server" ID="LB_IVA" Font-Bold="true"></asp:Label></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td><b>Total:</b></td>
                                                                        <td>
                                                                            <asp:Label runat="server" ID="LB_TOTAL" Font-Bold="true"></asp:Label></td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </div>
                                                        <!-- DIV ADJUJNTAR -->
                                                        <hr />
                                                        <h6 class="text-purple"><b><i class="fa fa-list"></i></b>Adjuntar archivos:</h6>
                                                        <div class="row">
                                                            <div class="col-sm-6">
                                                                <input id="FU_OC" style="font-size: 12px; margin-bottom: 5px;" type="file" />
                                                                <a onclick="SubirDocumento3();"><span class="btn btn-primary btn-block btn-sm"><i class="fa fa-upload"></i>&nbsp;Subir Documento seleccionado</span></a>
                                                                <label id="L_UP3"></label>
                                                                <div style="display: none">
                                                                    <asp:TextBox runat="server" ID="T_REV_NOMBRE_BD"></asp:TextBox>
                                                                    <asp:LinkButton ClientIDMode="Static" runat="server" ID="B_CARGARADJUNTOS" OnClick="B_CARGARADJUNTOS_Click"></asp:LinkButton>
                                                                </div>

                                                            </div>
                                                            <div class="col-sm-6">
                                                                <div class="table-responsive">
                                                                    <asp:GridView DataKeyNames="id_doc, id_oc" ClientIDMode="AutoID" runat="server"
                                                                        ID="G_ADJUNTOS" CssClass="table tablaprincipal table-bordered table-condensed table-sm"
                                                                        OnRowCommand="G_ADJUNTOS_RowCommand" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true"
                                                                        Font-Size="Small">
                                                                        <HeaderStyle CssClass="thead-dark" />
                                                                        <Columns>
                                                                            <asp:BoundField DataField="id_doc" Visible="false" />
                                                                            <asp:BoundField DataField="id_oc" Visible="false" />
                                                                            <asp:BoundField DataField="nom_real" HeaderText="NOMBRE ADJUNTO" />
                                                                            <asp:BoundField DataField="fecha" HeaderText="FECHA" />
                                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="" ItemStyle-HorizontalAlign="Center">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="veradjunto" ToolTip="Ver Adjunto">
                                                                                <i class="fa fa-file fa-2x text-primary"></i>
                                                                                    </asp:LinkButton>
                                                                                    &nbsp;&nbsp;
                                                                                <asp:LinkButton runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="Borrar" OnClientClick="return confirm('Desea eliminar el registro seleccionado?');">
                                                                                <i class="fa fa-trash fa-2x text-danger"></i>
                                                                                </asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <EmptyDataTemplate>
                                                                            No se encontraron resultados.
                                                                        </EmptyDataTemplate>
                                                                    </asp:GridView>
                                                                </div>

                                                            </div>
                                                        </div>
                                                    </div>
                                                    <hr />
                                                    <div class="row">
                                                        <div class="col-sm-12 text-right">
                                                            <asp:LinkButton ClientIDMode="AutoID" CssClass="btn btn-raised btn-primary btn-round waves-effect" runat="server" ID="B_GUARDAROC" OnClick="B_GUARDAROC_Click"><i class="fa fa-save"></i>&nbsp;GUARDAR ORDEN DE COMPRA</asp:LinkButton>
                                                            <asp:LinkButton ClientIDMode="AutoID" CssClass="btn btn-raised btn-default btn-round waves-effect" runat="server" ID="B_VOLVER" OnClick="B_VOLVER_Click"><i class="fa fa-undo"></i>&nbsp;VOLVER</asp:LinkButton>

                                                        </div>
                                                    </div>
                                                </form>
                                            </asp:Panel>
                                            <!-- DETALLE -->
                                            <asp:Panel ID="PANEL_DET" runat="server" Visible="false">
                                                <h4>
                                                    <b>
                                                        <i class="fa fa-file-invoice"></i>&nbsp;<asp:Label runat="server" ID="LBL_DETALLE" Text="Detalle/Glosa de OC Nº : "></asp:Label></b>
                                                </h4>
                                                <hr />
                                                <form class="form-horizontal">
                                                    <div style="display: none">
                                                        <asp:TextBox runat="server" ID="T_ID_DETALLE"></asp:TextBox>
                                                        <asp:TextBox runat="server" ID="T_ID_OC_DETALLE"></asp:TextBox>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-12">
                                                            <asp:GridView ClientIDMode="AutoID" runat="server"
                                                                ID="GG_DET" CssClass="table table-sm"
                                                                AutoGenerateColumns="false" ShowHeaderWhenEmpty="true"
                                                                Font-Size="Small">
                                                                <HeaderStyle CssClass="thead-dark" />
                                                                <Columns>
                                                                    <asp:BoundField DataField="id_oc_det" Visible="false" />
                                                                    <asp:BoundField DataField="id_oc" Visible="false" />
                                                                    <asp:BoundField DataField="li" HeaderText="LI" />
                                                                    <asp:BoundField DataField="cant" HeaderText="CANT" />
                                                                    <asp:BoundField DataField="um" HeaderText="UM" />
                                                                    <asp:BoundField DataField="glosa" HeaderText="GLOSA" />
                                                                    <asp:BoundField DataField="unitario" HeaderText="$ UNIT." DataFormatString="{0:C0}" />
                                                                    <asp:BoundField DataField="neto" HeaderText="NETO" DataFormatString="{0:C0}" ItemStyle-Font-Bold="true" />
                                                                    <asp:BoundField DataField="iva" HeaderText="IVA" DataFormatString="{0:C0}" ItemStyle-Font-Bold="true" />
                                                                    <asp:BoundField DataField="total" HeaderText="TOTAL" DataFormatString="{0:C0}" ItemStyle-Font-Bold="true" />
                                                                </Columns>
                                                                <EmptyDataTemplate>
                                                                    No se encontraron resultados.
                                                                </EmptyDataTemplate>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <%--  <div class="col-sm-4">
                                                            <small><b>Autorizado M. Zapata</b></small>
                                                            <asp:DropDownList runat="server" ID="CB_AUT_ZAPATA" CssClass="form-control combopro" Enabled="false">
                                                                <asp:ListItem Text="NO" Value="NO"></asp:ListItem>
                                                                <asp:ListItem Text="SI" Value="SI"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>--%>
                                                        <div class="col-sm-4">
                                                            <small><b>Autorizada</b></small>
                                                            <asp:DropDownList runat="server" ID="CB_AUT_ZAMORA" CssClass="form-control combopro" Enabled="false">
                                                                <asp:ListItem Text="NO" Value="NO"></asp:ListItem>
                                                                <asp:ListItem Text="SI" Value="SI"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-4">
                                                            <small><b>Estado</b></small>
                                                            <asp:DropDownList runat="server" ID="CB_ESTADO_OC" CssClass="form-control combopro" Enabled="false">
                                                            </asp:DropDownList>
                                                        </div>
                                                        <%--   <div class="col-sm-4">
                                                            <small><b>Autorizada</b></small>
                                                            <asp:DropDownList runat="server" ID="CB_AUTORIZADA" CssClass="form-control combopro" Enabled="false">
                                                                <asp:ListItem Text="NO" Value="NO" Selected="True"></asp:ListItem>
                                                                <asp:ListItem Text="SI" Value="SI"></asp:ListItem>
                                                                <asp:ListItem Text="RECHAZADA" Value="RECHAZADA"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>--%>
                                                        <div class="col-sm-4">
                                                            <small><b>Cancelada</b></small>
                                                            <asp:DropDownList runat="server" ID="CB_CANCELADA" CssClass="form-control combopro" Enabled="false">
                                                                <asp:ListItem Text="NO" Value="NO" Selected="True"></asp:ListItem>
                                                                <asp:ListItem Text="ABONADA" Value="ABONADA"></asp:ListItem>
                                                                <asp:ListItem Text="SI" Value="SI"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-sm-4">
                                                            <small><b>Facturada</b></small>
                                                            <asp:DropDownList runat="server" ID="CB_FACTURADA" CssClass="form-control combopro" Enabled="false">
                                                                <asp:ListItem Text="NO" Value="NO" Selected="True"></asp:ListItem>
                                                                <asp:ListItem Text="SI" Value="SI"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-sm-4">
                                                            <small><b>Nº Factura</b></small>
                                                            <asp:TextBox runat="server" ID="T_DET_NUM_FACTURA" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-4">
                                                            <small><b>Sist Fact.</b></small>
                                                            <asp:DropDownList runat="server" ID="CB_SIST_FACTURADA" CssClass="form-control combopro" Enabled="false">
                                                                <asp:ListItem Text="NO" Value="NO" Selected="True"></asp:ListItem>
                                                                <asp:ListItem Text="SI" Value="SI"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="row clearfix">
                                                        <div class="col-sm-12">
                                                            <div class="form-group">
                                                                <small><b>Observación</b></small>
                                                                <asp:TextBox runat="server" ID="T_DET_OBSERVACION" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <hr />
                                                    <div class="row">
                                                        <div class="col-sm-12 text-right">
                                                            <asp:LinkButton ClientIDMode="AutoID" CssClass="btn btn-raised btn-primary btn-round waves-effect" runat="server" ID="B_GUARDAR_DETALLE" OnClick="B_GUARDAR_DETALLE_Click"><i class="fa fa-save"></i>&nbsp;GUARDAR DETALLE GLOSA</asp:LinkButton>
                                                            <asp:LinkButton ClientIDMode="AutoID" CssClass="btn btn-raised btn-default btn-round waves-effect" runat="server" ID="B_VOLVERDET" OnClick="B_VOLVER_Click"><i class="fa fa-undo"></i>&nbsp;VOLVER</asp:LinkButton>
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

        function ImprimeOC(num_oc) {
            var win = window.open('Imprime_oc.aspx?num_oc=' + num_oc, '_blank');
            win.focus();
        }

        function ImprimeOC2() {
            var num_oc = document.getElementById('<%= T_ID.ClientID %>').value;
            var win = window.open('Imprime_oc.aspx?num_oc=' + num_oc, '_blank');
            win.focus();
        }

        function guid() {
            function s4() {
                return Math.floor((1 + Math.random()) * 0x10000)
                    .toString(16)
                    .substring(1);
            }
            return s4() + s4() + s4();
        }

        function SubirDocumento3() {
            var data = new FormData();
            var files = $("#FU_OC").get(0).files;
            var guid_pro = guid();
            if (files.length > 0) {
                var id_oc = document.getElementById('<%= T_ID.ClientID %>').value;
                data.append("UploadedImage", files[0]);
                var ajaxRequest = $.ajax({
                    type: "POST",
                    url: "SUBE_ARCHIVOS.aspx?tipo_doc=4&id_oc=" + id_oc + "&guid=" + guid_pro,
                    contentType: false,
                    processData: false,
                    data: data
                });
                ajaxRequest.done(function (xhr, textStatus) {
                    MostrarNotificacion('Documento cargado con éxito', 1);
                    document.getElementById('B_CARGARADJUNTOS').click();
                });
            }
            else {
                MostrarNotificacion('Error al subir archivo', 0);
            }
        }

        function VerArchivo(archivo) {
            //var win = window.open('Documentos/Camiones/MANT/' + id_camion + '/' + archivo, '_blank');
            var win = window.open(archivo, '_blank');
            win.focus();
        }

    </script>
</asp:Content>
