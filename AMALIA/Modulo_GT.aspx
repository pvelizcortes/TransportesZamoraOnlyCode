<%@ Page Title="" Language="C#" EnableEventValidation="false" MasterPageFile="~/Amalia.Master" AutoEventWireup="true" CodeBehind="Modulo_GT.aspx.cs" Inherits="AMALIA.Modulo_GT" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BODY" runat="server">

    <section class="content">
        <div class="container">
            <div class="row clearfix">
                <div class="col-lg-12">
                    <div class="card">
                        <div class="body block-header">
                            <div class="row">
                                <div class="col-lg-12 col-md-12 col-sm-12">
                                    <asp:UpdatePanel runat="server" ID="UP_NUEVO" UpdateMode="Conditional">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="B_NUEVO" EventName="Click" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <span style="font-size: large"><strong><i class="fa fa-shipping-fast"></i>&nbsp;Modulo GT </strong></span>
                                            &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                                                   
                                            <asp:LinkButton runat="server" ID="B_NUEVO" ClientIDMode="AutoID" CssClass="btn btn-primary btn-sm" OnClick="B_NUEVO_Click"><i class="fa fa-shipping-fast"></i>&nbsp;CREAR NUEVO VIAJE</asp:LinkButton>
                                            <ul class="breadcrumb p-l-0 p-b-0 float-right">
                                                <li class="breadcrumb-item"><a href="index.aspx"><i class="icon-home"></i></a></li>
                                                <li class="breadcrumb-item"><a href="index.aspx">Index</a></li>
                                                <li class="breadcrumb-item active">Modulo GT</li>
                                            </ul>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <hr />
                            <asp:UpdatePanel runat="server" ID="UP_PRINCIPAL" UpdateMode="Conditional">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="G_PRINCIPAL" EventName="RowCommand" />
                                    <asp:AsyncPostBackTrigger ControlID="B_GUARDAR" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="B_VOLVER" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="ABRE_MODAL_COMBUSTIBLE" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="B_FILTRAR" EventName="Click" />
                                </Triggers>
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-lg-12 col-md-12 col-sm-12">
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
                                                        GT DESDE:
                                                       
                                                        <br />
                                                        <asp:TextBox runat="server" ID="FILTRA_GT_DESDE" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        GT HASTA:
                                                       
                                                        <br />
                                                        <asp:TextBox runat="server" ID="FILTRA_GT_HASTA" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1">
                                                        <br />
                                                        <asp:LinkButton runat="server" ID="B_FILTRAR" ClientIDMode="AutoID" OnClick="B_FILTRAR_Click" CssClass="btn btn-primary btn-sm btn-round"><i class="fa fa-search"></i> Filtrar</asp:LinkButton>
                                                    </div>
                                                </div>
                                                <hr />
                                                <div class="body table-responsive">
                                                    <asp:GridView DataKeyNames="ID_GT, saldo_total, id_estado, saldo_dinero_entregado_new, entregado" OnRowDataBound="G_PRINCIPAL_RowDataBound" ClientIDMode="AutoID" runat="server" ID="G_PRINCIPAL" CssClass="table table-bordered tablaprincipal table-hover js-exportable table-condensed" OnRowCommand="G_PRINCIPAL_RowCommand" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true">
                                                        <HeaderStyle CssClass="thead-dark" />
                                                        <Columns>
                                                            <asp:BoundField HeaderText="ID_GT" DataField="ID_GT" Visible="false" />
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Opciones" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ToolTip="Editar" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="Editar" OnClientClick="relojito(true);">
                                                                   <i class="icon-pencil"></i>
                                                                    </asp:LinkButton>
                                                                    &nbsp;
                                                                    &nbsp;
                                                                    
                                                                    <asp:LinkButton ToolTip="Resumen" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="Resumen" OnClientClick='<%# String.Format("return ResumenGT({0});", Eval("ID_GT")) %>'>
                                                                   <i class="fa fa-eye"></i>
                                                                     </asp:LinkButton>
                                                                    &nbsp;
                                                                    &nbsp;
                                                                    
                                                                    <asp:LinkButton ToolTip="Imprimir PDF" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="Print" OnClientClick='<%# String.Format("return ImprimeGT({0});", Eval("ID_GT")) %>'>
                                                                   <i class="fa fa-print"></i>
                                                                     </asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:BoundField HeaderText="Estado" DataField="id_estado" Visible="false" />
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Estado" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <div id="div_estado" runat="server"></div>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="GT" DataField="num_correlativo" ItemStyle-Font-Bold="true" />
                                                            <asp:BoundField HeaderText="Usuario" DataField="usuario" />
                                                            <asp:BoundField HeaderText="Fecha Inicio" DataField="fecha_inicio" DataFormatString="{0:dd/MM/yyyy}" />
                                                            <asp:BoundField HeaderText="Fecha Término" DataField="fecha_termino" DataFormatString="{0:dd/MM/yyyy}" />
                                                            <asp:BoundField HeaderText="Conductor" DataField="conductor1" />
                                                            <asp:BoundField HeaderText="Patente" DataField="patente_camion" />
                                                            <asp:BoundField HeaderText="Km. Totales" DataField="total_km" DataFormatString="{0:N0}" />
                                                            <asp:BoundField HeaderText="Rendimiento" DataField="rendimiento" DataFormatString="{0:N3}" />
                                                            <asp:BoundField HeaderText="Dinero Entregado" DataField="dinero_entregado" DataFormatString="{0:C0}" ItemStyle-Font-Bold="true" />
                                                            <%--<asp:BoundField HeaderText="Sobre Deposito" DataField="sobre_deposito" DataFormatString="{0:C0}" ItemStyle-Font-Bold="true" />--%>
                                                            <asp:BoundField HeaderText="Saldo" DataField="saldo_dinero_entregado_new" ItemStyle-Font-Bold="true" DataFormatString="{0:C0}" />
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Estado" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <div id="div_entregado" runat="server"></div>
                                                                    <span>
                                                                        <asp:LinkButton runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="EstadoDineroEntregado"><i class="fa fa-exchange-alt"></i></asp:LinkButton></span>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="Ganancia" DataField="saldo_total" ItemStyle-Font-Bold="true" DataFormatString="{0:C0}" />
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ToolTip="Borrar" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="Borrar" OnClientClick="return confirm('Desea eliminar la GT seleccionada? las OTZ asociadas se eliminarán automaticamente.');">
                                                                   <i class="icon-trash"></i>
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="entregado" DataField="entregado" Visible="false" />
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            No se encontraron resultados.
                                                       
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                </div>
                                            </asp:Panel>
                                            <asp:Panel runat="server" ID="PANEL_DETALLE1" Visible="false">
                                                <div class="body">
                                                    <div class="row">
                                                        <div class="col-sm-6">
                                                            <div class="form-horizontal">
                                                                <div style="display: none">
                                                                    <asp:TextBox runat="server" ID="T_ID"></asp:TextBox>
                                                                    <a id="MODAL_COMBUSTIBLE" href="#modalcombustible" data-toggle="modal" data-target="#modalcombustible"></a>
                                                                    <a id="MODAL_GASTO" href="#modalgasto" data-toggle="modal" data-target="#modalgasto"></a>
                                                                    <a id="MODAL_OTZ" href="#modalotz" data-toggle="modal" data-target="#modalotz"></a>
                                                                </div>
                                                                <div class="row clearfix">
                                                                    <div class="col-sm-4 form-control-label">
                                                                        <h2>NUMERO <strong class=" text-purple">GT</strong></h2>
                                                                    </div>
                                                                    <div class="col-sm-4">
                                                                        <div class="form-group">
                                                                            <h2>
                                                                                <asp:TextBox runat="server" ID="NUM_GT" CssClass="tz-input"></asp:TextBox></h2>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row clearfix">
                                                                    <div class="col-sm-4 form-control-label">
                                                                        <label><b><i class="fa fa-star  text-purple"></i>&nbsp;&nbsp;&nbsp;Estado GT</b></label>
                                                                    </div>
                                                                    <div class="col-sm-6">
                                                                        <div class="form-group">
                                                                            <asp:DropDownList ID="CB_ESTADO_GT" runat="server" CssClass="form-control combopro">
                                                                                <asp:ListItem Text="Abierta" Value="1" Selected="True"></asp:ListItem>
                                                                                <asp:ListItem Text="Viaje Terminado" Value="2"></asp:ListItem>
                                                                                <asp:ListItem Text="Cerrada" Value="3"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <hr />
                                                                <div class="row clearfix">
                                                                    <div class="col-sm-4 form-control-label">
                                                                        <label><b><i class="fa fa-user  text-purple"></i>&nbsp;&nbsp;&nbsp;Conductor</b></label>
                                                                    </div>
                                                                    <div class="col-sm-6">
                                                                        <div class="form-group">
                                                                            <asp:DropDownList ID="CB_CONDUCTOR" Enabled="false" runat="server" CssClass="form-control combopro">
                                                                                <asp:ListItem Text="-- seleccione --" Value="0" Selected="True"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <small>(Se manejará desde depositos)</small>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row clearfix">
                                                                    <div class="col-sm-4 form-control-label">
                                                                        <label><b><i class="fa fa-user  text-purple"></i>&nbsp;&nbsp;&nbsp;Conductor 2</b></label>
                                                                    </div>
                                                                    <div class="col-sm-6">
                                                                        <div class="form-group">
                                                                            <asp:DropDownList ID="CB_CONDUCTOR2" Enabled="false" runat="server" CssClass="form-control combopro">
                                                                                <asp:ListItem Text="-- seleccione --" Value="0" Selected="True"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <small>(Se manejará desde depositos)</small>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <hr />
                                                                <div class="row clearfix">
                                                                    <div class="col-sm-4 form-control-label">
                                                                        <label><b><i class="fa fa-truck  text-purple"></i>&nbsp;&nbsp;&nbsp;Patente</b></label>
                                                                    </div>
                                                                    <div class="col-sm-6">
                                                                        <div class="form-group">
                                                                            <asp:DropDownList ID="CB_CAMION" runat="server" CssClass="form-control combopro">
                                                                                <asp:ListItem Text="-- seleccione --" Value="0" Selected="True"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>

                                                                </div>
                                                                <div class="row clearfix mb-2">
                                                                    <div class="col-sm-4 form-control-label">
                                                                        <label><b><i class="fa fa-truck  text-purple"></i>&nbsp;&nbsp;&nbsp;Tipo Camión</b></label>
                                                                    </div>
                                                                    <div class="col-sm-6">
                                                                        <div class="form-group">
                                                                            <asp:TextBox runat="server" ID="T_TIPO_CAMION" CssClass="tz-input" placeholder="Ingrese tipo de camión..."></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row clearfix">
                                                                    <div class="col-sm-4 form-control-label">
                                                                        <label><b><i class="fa fa-pallet text-purple"></i>&nbsp;&nbsp;&nbsp;Rampla</b></label>
                                                                    </div>
                                                                    <div class="col-sm-6">
                                                                        <div class="form-group">
                                                                            <asp:DropDownList ID="CB_RAMPLA" runat="server" CssClass="form-control combopro">
                                                                                <asp:ListItem Text="-- seleccione --" Value="0" Selected="True"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <div class="row clearfix">
                                                                <div class="col-sm-4 form-control-label">
                                                                    <label><b><i class="fa fa-calendar text-purple"></i>&nbsp;&nbsp;&nbsp;Fecha Creación</b></label>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <div class="form-group">
                                                                        <asp:TextBox runat="server" Enabled="false" TextMode="Date" ID="T_FECHA_CREACION" CssClass="input-sm"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row clearfix">
                                                                <div class="col-sm-4 form-control-label">
                                                                    <label><b><i class="fa fa-calendar text-purple"></i>&nbsp;&nbsp;&nbsp;Fecha Inicio</b></label>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <div class="form-group">
                                                                        <asp:TextBox runat="server" TextMode="Date" ID="T_FECHA_INICIO" CssClass="tz-input"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row clearfix">
                                                                <div class="col-sm-4 form-control-label">
                                                                    <label><b><i class="fa fa-calendar  text-purple"></i>&nbsp;&nbsp;&nbsp;Fecha Término</b></label>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <div class="form-group">
                                                                        <asp:TextBox runat="server" TextMode="Date" ID="T_FECHA_FINAL" CssClass="tz-input"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <hr />
                                                            <div class="row clearfix">
                                                                <div class="col-sm-4 form-control-label">
                                                                    <label><b><i class="fa fa-arrow-right  text-purple"></i>&nbsp;&nbsp;&nbsp;Km Inicial</b></label>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <div class="form-group">
                                                                        <asp:TextBox runat="server" TextMode="Number" ID="T_KM_INICIAL" CssClass="tz-input"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row clearfix">
                                                                <div class="col-sm-4 form-control-label">
                                                                    <label><b><i class="fa fa-arrow-left  text-purple"></i>&nbsp;&nbsp;&nbsp;Km Final</b></label>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <div class="form-group">
                                                                        <asp:TextBox runat="server" TextMode="Number" ID="T_KM_FINAL" CssClass="tz-input"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <hr />
                                                            <div class="row clearfix">
                                                                <div class="col-sm-4 form-control-label">
                                                                    <label><b><i class="fa fa-dollar-sign  text-purple"></i>&nbsp;&nbsp;&nbsp;Dinero Entregado</b></label>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <div class="form-group">
                                                                        <asp:TextBox runat="server" TextMode="Number" ID="T_DINERO_ENTREGADO" CssClass="tz-input" Text="0" ReadOnly="true"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <%-- <div class="row clearfix">
                                                                <div class="col-sm-4 form-control-label">
                                                                    <label><b><i class="fa fa-dollar-sign  text-purple"></i>&nbsp;&nbsp;&nbsp;Sobre Depósito</b></label>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <div class="form-group">
                                                                        &nbsp;
                                                                        <%--<asp:TextBox runat="server" TextMode="Number" ID="T_SOBREDEPOSITO" CssClass="tz-input"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>--%>
                                                            <div class="row clearfix">
                                                                <div class="col-sm-4 form-control-label">
                                                                    <label><b><i class="fa fa-dollar-sign  text-purple"></i>&nbsp;&nbsp;&nbsp;Dinero Devuelto</b></label>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <div class="form-group">
                                                                        <asp:TextBox runat="server" ID="T_DINERO_DEVUELTO" TextMode="Number" Font-Bold="true" CssClass="tz-input" ReadOnly="true"></asp:TextBox>
                                                                        <br />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row clearfix">
                                                                <div class="col-sm-4 form-control-label">
                                                                    <label><b><i class="fa fa-dollar-sign  text-purple"></i>&nbsp;&nbsp;&nbsp;Saldo</b></label>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <div class="form-group">
                                                                        <asp:TextBox runat="server" ID="T_SALDO_DEPOSITO" ReadOnly="true" Font-Bold="true" CssClass="tz-input"></asp:TextBox>
                                                                        <br />
                                                                        <div class="checkbox m-t-10">
                                                                            <asp:CheckBox runat="server" ID="CHK_ENTREGADO" ClientIDMode="Static" />
                                                                            <label for="CHK_ENTREGADO">
                                                                                Entregado
                                                                           
                                                                            </label>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-sm-12">
                                                            <div class="form-horizontal">
                                                                <div class="row clearfix">
                                                                    <div class="col-sm-2 form-control-label">
                                                                        <label><b><i class="fa fa-calendar text-purple"></i>&nbsp;&nbsp;&nbsp;Observación</b></label>
                                                                    </div>
                                                                    <div class="col-sm-8">
                                                                        <div class="form-group">
                                                                            <asp:TextBox runat="server" ID="T_OBSERVACION" CssClass="tz-input"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <hr />
                                                    <div id="DIV_OTZ" runat="server">
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <span style="font-size: large"><i class="fa fa-truck-loading"></i>&nbsp;Servicios <strong>OTZ</strong> </span>&nbsp;&nbsp;
                                                                      
                                                                <asp:LinkButton runat="server" ID="B_AGREGAR_OTZ" OnClick="B_AGREGAR_OTZ_Click" CssClass="btn btn-primary btn-sm">+ AGREGAR</asp:LinkButton>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <div class="table-responsive">
                                                                    <asp:GridView DataKeyNames="ID_OTZ, ID_GT" runat="server" ClientIDMode="AutoID" ID="G_OTZ" CssClass="table table-bordered tablaprincipal" OnRowCommand="G_OTZ_RowCommand" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true">
                                                                        <HeaderStyle CssClass="thead-light" />
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="" ItemStyle-HorizontalAlign="Center">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="Editar">
                                                                                    <i class="icon-pencil"></i>
                                                                                    </asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField HeaderText="ID_OTZ" DataField="ID_OTZ" Visible="false" />
                                                                            <asp:BoundField HeaderText="ID_GT" DataField="ID_GT" Visible="false" />
                                                                            <asp:BoundField HeaderText="OTZ" DataField="correlativo_otz" ItemStyle-Font-Bold="true" />
                                                                            <asp:BoundField HeaderText="Origen" DataField="c_origen" />
                                                                            <asp:BoundField HeaderText="Destino" DataField="c_destino" />
                                                                            <asp:BoundField HeaderText="Obra" DataField="obra" />
                                                                            <asp:BoundField HeaderText="Cliente" DataField="nombre_cliente" />
                                                                            <asp:BoundField HeaderText="F. Inicio" DataField="fecha_inicio" DataFormatString="{0:dd/MM/yyyy}" />
                                                                            <asp:BoundField HeaderText="F. Final" DataField="fecha_final" DataFormatString="{0:dd/MM/yyyy}" />
                                                                            <asp:BoundField HeaderText="Valor" DataField="valor_viaje" DataFormatString="{0:C0}" ItemStyle-ForeColor="Green" ItemStyle-Font-Bold="true" />
                                                                            <asp:BoundField HeaderText="Estadía" DataField="estadia" DataFormatString="{0:C0}" ItemStyle-ForeColor="Green" ItemStyle-Font-Bold="true" />
                                                                            <asp:BoundField HeaderText="Entradas" DataField="entradas" DataFormatString="{0:C0}" ItemStyle-ForeColor="Green" ItemStyle-Font-Bold="true" />
                                                                            <asp:BoundField HeaderText="Doble Conductor" DataField="doble_Conductor" DataFormatString="{0:C0}" ItemStyle-ForeColor="Green" ItemStyle-Font-Bold="true" />
                                                                            <asp:BoundField HeaderText="Carga Descarga" DataField="carga_descarga" DataFormatString="{0:C0}" ItemStyle-ForeColor="Green" ItemStyle-Font-Bold="true" />
                                                                            <asp:BoundField HeaderText="Otros" DataField="otros" DataFormatString="{0:C0}" ItemStyle-ForeColor="Green" ItemStyle-Font-Bold="true" />
                                                                            <asp:BoundField HeaderText="Detalle Otros" DataField="detalle_otros" />
                                                                            <asp:BoundField HeaderText="Flete de Tercero" DataField="flete_de_tercero" DataFormatString="{0:C0}" ItemStyle-ForeColor="Red" ItemStyle-Font-Bold="true" />
                                                                            <asp:BoundField HeaderText="TOTAL" DataField="suma_otz" DataFormatString="{0:C0}" ItemStyle-Font-Bold="true" ItemStyle-ForeColor="Green" />
                                                                            <asp:BoundField HeaderText="Nombre Tercero" DataField="d_nombre_tercero" />
                                                                            <asp:BoundField HeaderText="EEPP Tercero" DataField="d_eepp_tercero" />
                                                                            <asp:BoundField HeaderText="Factura Tercero" DataField="d_factura_tercero" />
                                                                            <asp:BoundField HeaderText="Diferencia Factura" DataField="diferencia_factura" DataFormatString="{0:C0}" ItemStyle-ForeColor="Green" ItemStyle-Font-Bold="true" />
                                                                            <asp:BoundField HeaderText="Observación Factura" DataField="observacion_factura" />
                                                                            <asp:BoundField HeaderText="SOL-OC" DataField="d_sol_oc" />
                                                                            <asp:BoundField HeaderText="OT" DataField="d_ot" />
                                                                            <asp:BoundField HeaderText="Guia Despacho" DataField="guia" />
                                                                            <asp:BoundField HeaderText="EEPP" DataField="d_eepp" />
                                                                            <asp:BoundField HeaderText="Gasto" DataField="d_gasto" />
                                                                            <asp:BoundField HeaderText="OC" DataField="d_oc" />
                                                                            <asp:BoundField HeaderText="HES" DataField="d_hes" />
                                                                            <asp:BoundField HeaderText="Factura" DataField="d_factura" />
                                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="Borrar" OnClientClick="return confirm('Desea eliminar la OTZ seleccionada?');">
                                                                                    <i class="icon-trash"></i>
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
                                                        <hr />
                                                    </div>
                                                    <div id="DIV_COMBUSTIBLE" runat="server">
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <span style="font-size: large"><i class="material-icons">local_gas_station</i>&nbsp;Gasto de <strong>Combustible </strong></span>&nbsp;&nbsp;  
                                                           
                                                                <asp:LinkButton runat="server" ID="ABRE_MODAL_COMBUSTIBLE" OnClick="AGREGAR_COMBUSTIBLE_Click" CssClass="btn btn-primary btn-sm">+ AGREGAR</asp:LinkButton>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <div class="table-responsive">
                                                                    <asp:GridView DataKeyNames="ID_CARGA" runat="server" ClientIDMode="AutoID" ID="G_COMBUSTIBLE" CssClass="table table-bordered tablaprincipal" OnRowCommand="G_COMBUSTIBLE_RowCommand" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true">
                                                                        <HeaderStyle CssClass="thead-light" />
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="" ItemStyle-HorizontalAlign="Center">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="Editar">
                                                                                    <i class="icon-pencil"></i>
                                                                                    </asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField HeaderText="ID_CARGA" DataField="ID_CARGA" Visible="false" />
                                                                            <asp:BoundField HeaderText="Estación" DataField="nombre_estacion" />
                                                                            <asp:BoundField HeaderText="Fecha" DataField="fecha" DataFormatString="{0:dd/MM/yyyy}" />
                                                                            <asp:BoundField HeaderText="Guía" DataField="guia" />
                                                                            <asp:BoundField HeaderText="Rollo" DataField="rollo" />
                                                                            <asp:BoundField HeaderText="Km Odometro" DataField="kilometraje" DataFormatString="{0:N0}" />
                                                                            <asp:BoundField HeaderText="Lts." DataField="litros_cargados" DataFormatString="{0:N2}" />
                                                                            <asp:BoundField HeaderText="Precio" DataField="precio" DataFormatString="{0:C0}" ItemStyle-ForeColor="Red" ItemStyle-Font-Bold="true" />
                                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="" ItemStyle-HorizontalAlign="Center">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="Borrar" OnClientClick="return confirm('Desea eliminar el registro seleccionado?');">
                                                                                    <i class="icon-trash"></i>
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
                                                        <hr />
                                                    </div>
                                                    <div id="DIV_GASTOS" runat="server">
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <span style="font-size: large"><i class="material-icons">attach_money</i>&nbsp;Gastos <strong>Generales </strong></span>&nbsp;&nbsp;
                                                                      
                                                                <asp:LinkButton runat="server" ID="B_AGREGAR_GASTOGRAL" OnClick="B_AGREGAR_GASTOGRAL_Click" CssClass="btn btn-primary btn-sm">+ AGREGAR</asp:LinkButton>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <div class="table-responsive">
                                                                    <asp:GridView DataKeyNames="ID_GASTO" runat="server" ClientIDMode="AutoID" ID="G_GASTOGRAL" CssClass="table table-bordered tablaprincipal" OnRowCommand="G_GASTOGRAL_RowCommand" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true">
                                                                        <HeaderStyle CssClass="thead-light" />
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="" ItemStyle-HorizontalAlign="Center">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="Editar">
                                                                                    <i class="icon-pencil"></i>
                                                                                    </asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField HeaderText="ID_GASTO" DataField="ID_GASTO" Visible="false" />
                                                                            <asp:BoundField HeaderText="Tipo de Gasto" DataField="nombre_tipo_gasto" />
                                                                            <asp:BoundField HeaderText="Detalle" DataField="detalle" />
                                                                            <asp:BoundField HeaderText="Valor" DataField="valor" DataFormatString="{0:C0}" ItemStyle-ForeColor="Red" ItemStyle-Font-Bold="true" />
                                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="" ItemStyle-HorizontalAlign="Center">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="Borrar" OnClientClick="return confirm('Desea eliminar el registro seleccionado?');">
                                                                                    <i class="icon-trash"></i>
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
                                                </div>
                                                <div class="text-center">
                                                    <h2>
                                                        <asp:LinkButton UseSubmitBehavior="false" ClientIDMode="AutoID" CssClass="btn btn-primary btn-round waves-effect" runat="server" ID="B_GUARDAR" OnClick="B_GUARDAR_Click" OnClientClick="relojito(true);"><i class="fa fa-save"></i>&nbsp;GUARDAR</asp:LinkButton>
                                                        <asp:LinkButton runat="server" ID="LinkButton1" ClientIDMode="AutoID" OnClientClick="ImprimeGT2();" CssClass="btn btn-primary btn-round waves-effect"><i class="fa fa-print"></i> VER PDF</asp:LinkButton>
                                                        <asp:LinkButton ClientIDMode="AutoID" CssClass="btn btn-simple btn-primary btn-round waves-effect" runat="server" ID="B_VOLVER" OnClick="B_VOLVER_Click"><i class="fa fa-undo"></i>&nbsp;VOLVER</asp:LinkButton>
                                                    </h2>
                                                </div>
                                                <hr />
                                                <br />
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <!-- MODALES -->
    <!-- MODAL OTZ -->
    <div class="modal fade" id="modalotz" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <asp:UpdatePanel runat="server" ID="UP_OTZ" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="B_GUARDAR_OTZ" EventName="Click" />
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="title text-purple" id="TITULO_MODAL_OTZ" runat="server">OTZ</h4>
                        </div>
                        <hr />
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="form-horizontal">
                                        <div style="display: none">
                                            <asp:TextBox runat="server" ID="T_ID_OTZ"></asp:TextBox>
                                        </div>
                                        <div class="row clearfix">
                                            <div class="col-sm-4 form-control-label">
                                                <label class="font-bold">OTZ</label>
                                            </div>
                                            <div class="col-sm-8">
                                                <div class="form-group">
                                                    <asp:TextBox runat="server" ID="T_OTZ_CORRELATIVO" CssClass="tz-input tz-md font-bold"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row clearfix">
                                            <div class="col-sm-4 form-control-label">
                                                <label class="font-bold">Cliente</label>
                                            </div>
                                            <div class="col-sm-8">
                                                <div class="form-group">
                                                    <asp:DropDownList runat="server" ID="CB_OTZ_CLIENTE" CssClass="form-control combopro"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row clearfix">
                                            <div class="col-sm-4 form-control-label">
                                                <label class="font-bold">Ciudad Origen</label>
                                            </div>
                                            <div class="col-sm-8">
                                                <div class="form-group">
                                                    <asp:DropDownList runat="server" ID="CB_CIUDAD_ORIGEN" CssClass="form-control combopro"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row clearfix">
                                            <div class="col-sm-4 form-control-label">
                                                <label class="font-bold">Ciudad Destino</label>
                                            </div>
                                            <div class="col-sm-8">
                                                <div class="form-group">
                                                    <asp:DropDownList runat="server" ID="CB_CIUDAD_DESTINO" CssClass="form-control combopro"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row clearfix">
                                            <div class="col-sm-4 form-control-label">
                                                <label class="font-bold">Obra</label>
                                            </div>
                                            <div class="col-sm-8">
                                                <div class="form-group">
                                                    <asp:DropDownList runat="server" ID="CB_OBRA" CssClass="form-control combopro"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row clearfix">
                                            <div class="col-sm-4 form-control-label">
                                                <label class="font-bold">Fecha Inicio</label>
                                            </div>
                                            <div class="col-sm-8">
                                                <div class="form-group">
                                                    <asp:TextBox runat="server" TextMode="Date" ID="T_OTZ_FECHA_INICIO" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row clearfix">
                                            <div class="col-sm-4 form-control-label">
                                                <label class="font-bold">Fecha Final</label>
                                            </div>
                                            <div class="col-sm-8">
                                                <div class="form-group">
                                                    <asp:TextBox runat="server" TextMode="Date" ID="T_OTZ_FECHA_FINAL" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row clearfix">
                                            <div class="col-sm-4 form-control-label">
                                                <label class="font-bold">Valor Viaje ($) </label>
                                            </div>
                                            <div class="col-sm-8">
                                                <div class="form-group">
                                                    <asp:TextBox runat="server" ID="T_OTZ_VALOR" TextMode="Number" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <hr />
                                        <h4>Documentos</h4>
                                        <div class="row clearfix">
                                            <div class="col-sm-4 form-control-label">
                                                <label class="font-bold">SOL - OC</label>
                                            </div>
                                            <div class="col-sm-8">
                                                <div class="form-group">
                                                    <asp:TextBox runat="server" ID="T_OTZ_SOL_OC" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row clearfix">
                                            <div class="col-sm-4 form-control-label">
                                                <label class="font-bold">OT</label>
                                            </div>
                                            <div class="col-sm-8">
                                                <div class="form-group">
                                                    <asp:TextBox runat="server" ID="T_OTZ_OT" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row clearfix">
                                            <div class="col-sm-4 form-control-label">
                                                <label class="font-bold">GUIA DESPACHO</label>
                                            </div>
                                            <div class="col-sm-8">
                                                <div class="form-group">
                                                    <asp:TextBox runat="server" ID="T_OTZ_GUIA" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row clearfix">
                                            <div class="col-sm-4 form-control-label">
                                                <label class="font-bold">EEPP</label>
                                            </div>
                                            <div class="col-sm-8">
                                                <div class="form-group">
                                                    <asp:TextBox runat="server" ID="T_OTZ_EEPP" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row clearfix">
                                            <div class="col-sm-4 form-control-label">
                                                <label class="font-bold">GASTO</label>
                                            </div>
                                            <div class="col-sm-8">
                                                <div class="form-group">
                                                    <asp:TextBox runat="server" ID="T_OTZ_GASTO" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row clearfix">
                                            <div class="col-sm-4 form-control-label">
                                                <label class="font-bold">OC</label>
                                            </div>
                                            <div class="col-sm-8">
                                                <div class="form-group">
                                                    <asp:TextBox runat="server" ID="T_OTZ_D_OC" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row clearfix">
                                            <div class="col-sm-4 form-control-label">
                                                <label class="font-bold">HES</label>
                                            </div>
                                            <div class="col-sm-8">
                                                <div class="form-group">
                                                    <asp:TextBox runat="server" ID="T_OTZ_HES" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row clearfix">
                                            <div class="col-sm-4 form-control-label">
                                                <label class="font-bold">FACTURA</label>
                                            </div>
                                            <div class="col-sm-8">
                                                <div class="form-group">
                                                    <asp:TextBox runat="server" ID="T_OTZ_FACTURA" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <hr />
                                        <h4>Opcionales</h4>
                                        <div class="row clearfix">
                                            <div class="col-sm-4 form-control-label">
                                                <label class="font-bold col-green">Estadía (+$) </label>
                                            </div>
                                            <div class="col-sm-8">
                                                <div class="form-group">
                                                    <asp:TextBox runat="server" ID="T_OTZ_ESTADIA" TextMode="Number" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row clearfix">
                                            <div class="col-sm-4 form-control-label">
                                                <label class="font-bold col-green">Entradas (+$) </label>
                                            </div>
                                            <div class="col-sm-8">
                                                <div class="form-group">
                                                    <asp:TextBox runat="server" ID="T_OTZ_ENTRADAS" TextMode="Number" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row clearfix">
                                            <div class="col-sm-4 form-control-label">
                                                <label class="font-bold col-green">Doble Conductor (+$) </label>
                                            </div>
                                            <div class="col-sm-8">
                                                <div class="form-group">
                                                    <asp:TextBox runat="server" ID="T_OTZ_DOBLE_CONDUCTOR" TextMode="Number" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row clearfix">
                                            <div class="col-sm-4 form-control-label">
                                                <label class="font-bold col-green">Carga / Descarga (+$) </label>
                                            </div>
                                            <div class="col-sm-8">
                                                <div class="form-group">
                                                    <asp:TextBox runat="server" ID="T_OTZ_CARGA_DESCARGA" TextMode="Number" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row clearfix">
                                            <div class="col-sm-4 form-control-label">
                                                <label class="font-bold col-green">Otros (+$) </label>
                                            </div>
                                            <div class="col-sm-8">
                                                <div class="form-group">
                                                    <asp:TextBox runat="server" ID="T_OTZ_OTROS" TextMode="Number" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row clearfix">
                                            <div class="col-sm-4 form-control-label">
                                                <label class="font-bold col-green">Descripción Otros </label>
                                            </div>
                                            <div class="col-sm-8">
                                                <div class="form-group">
                                                    <asp:TextBox runat="server" ID="T_OTZ_OTROS_DETALLE" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row clearfix">
                                            <div class="col-sm-4 form-control-label">
                                                <label class="font-bold col-red">Flete de Tercero (-$) </label>
                                            </div>
                                            <div class="col-sm-8">
                                                <div class="form-group">
                                                    <asp:TextBox runat="server" ID="T_OTZ_FLETE_DE_TERCERO" TextMode="Number" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row clearfix">
                                            <div class="col-sm-4 form-control-label">
                                                <label class="font-bold">Nombre de Tercero</label>
                                            </div>
                                            <div class="col-sm-8">
                                                <div class="form-group">
                                                    <asp:TextBox runat="server" ID="T_OTZ_NOMBRE_TERCERO" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row clearfix">
                                            <div class="col-sm-4 form-control-label">
                                                <label class="font-bold">EEPP Tercero</label>
                                            </div>
                                            <div class="col-sm-8">
                                                <div class="form-group">
                                                    <asp:TextBox runat="server" ID="T_OTZ_EEPP_TERCERO" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row clearfix">
                                            <div class="col-sm-4 form-control-label">
                                                <label class="font-bold">Factura de Tercero</label>
                                            </div>
                                            <div class="col-sm-8">
                                                <div class="form-group">
                                                    <asp:TextBox runat="server" ID="T_OTZ_FACTURA_DE_TERCERO" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row clearfix">
                                            <div class="col-sm-4 form-control-label">
                                                <label class="font-bold col-green">Diferencia Factura</label>
                                            </div>
                                            <div class="col-sm-8">
                                                <div class="form-group">
                                                    <asp:TextBox runat="server" TextMode="Number" ID="T_OTZ_DIFERENCIA_FACTURA" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row clearfix">
                                            <div class="col-sm-4 form-control-label">
                                                <label class="font-bold">Observación Factura</label>
                                            </div>
                                            <div class="col-sm-8">
                                                <div class="form-group">
                                                    <asp:TextBox runat="server" ID="T_OTZ_OBSERVACION_FACTURA" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:LinkButton runat="server" ID="B_GUARDAR_OTZ" ClientIDMode="AutoID" OnClick="B_GUARDAR_OTZ_Click" CssClass="btn btn-primary btn-round waves-effect"><i class="fa fa-save"></i> GUARDAR</asp:LinkButton>
                            <button type="button" class="btn btn-danger btn-simple btn-round waves-effect" data-dismiss="modal" id="CERRAR_MODAL_OTZ">Cerrar</button>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <!-- MODAL CARGA DE COMBUSTIBLE -->
    <div class="modal fade" id="modalcombustible" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="title text-purple">Carga de Combustible</h4>
                </div>
                <hr />
                <div class="modal-body">
                    <asp:UpdatePanel runat="server" ID="UP_COMBUSTIBLE" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="AGREGAR_COMBUSTIBLE" EventName="Click" />
                        </Triggers>
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="form-horizontal">
                                        <div style="display: none">
                                            <asp:TextBox runat="server" ID="T_ID_COMBUSTIBLE"></asp:TextBox>
                                        </div>
                                        <div class="row clearfix">
                                            <div class="col-sm-4 form-control-label">
                                                <label>Estación de Servicio</label>
                                            </div>
                                            <div class="col-sm-8">
                                                <div class="form-group">
                                                    <asp:DropDownList runat="server" ID="CB_ESTACION_SERVICIO" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row clearfix">
                                            <div class="col-sm-4 form-control-label">
                                                <label>Fecha</label>
                                            </div>
                                            <div class="col-sm-8">
                                                <asp:TextBox runat="server" TextMode="Date" ID="T_COMBUSTIBLE_FECHACARGA" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row clearfix">
                                            <div class="col-sm-4 form-control-label">
                                                <label>Guia</label>
                                            </div>
                                            <div class="col-sm-8">
                                                <div class="form-group">
                                                    <asp:TextBox runat="server" ID="T_COMBUSTIBLE_GUIA" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row clearfix">
                                            <div class="col-sm-4 form-control-label">
                                                <label>Rollo</label>
                                            </div>
                                            <div class="col-sm-8">
                                                <div class="form-group">
                                                    <asp:TextBox runat="server" ID="T_COMBUSTIBLE_ROLLO" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row clearfix">
                                            <div class="col-sm-4 form-control-label">
                                                <label>Km. (Odometro)</label>
                                            </div>
                                            <div class="col-sm-8">
                                                <div class="form-group">
                                                    <asp:TextBox runat="server" ID="T_COMBUSTIBLE_KM" TextMode="Number" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row clearfix">
                                            <div class="col-sm-4 form-control-label">
                                                <label>Litros Cargados</label>
                                            </div>
                                            <div class="col-sm-8">
                                                <div class="form-group">
                                                    <asp:TextBox runat="server" ID="T_COMBUSTIBLE_LITROS" TextMode="Number" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row clearfix">
                                            <div class="col-sm-4 form-control-label">
                                                <label>Precio</label>
                                            </div>
                                            <div class="col-sm-8">
                                                <div class="form-group">
                                                    <asp:TextBox runat="server" ID="T_COMBUSTIBLE_PRECIO" TextMode="Number" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                    <asp:LinkButton runat="server" ID="AGREGAR_COMBUSTIBLE" ClientIDMode="AutoID" OnClick="GUARDAR_COMBUSTIBLE_Click" CssClass="btn btn-primary btn-round waves-effect"><i class="fa fa-save"></i> GUARDAR</asp:LinkButton>
                    <button type="button" class="btn btn-danger btn-simple btn-round waves-effect" data-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>
    </div>
    <!-- MODAL CARGA DE GASTO GENERAL -->
    <div class="modal fade" id="modalgasto" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="title text-purple">Gasto General</h4>
                </div>
                <hr />
                <div class="modal-body">
                    <asp:UpdatePanel runat="server" ID="UP_GASTO_GENERAL" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="B_GUARDAR_GASTOSGRAL" EventName="Click" />
                        </Triggers>
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="form-horizontal">
                                        <div style="display: none">
                                            <asp:TextBox runat="server" ID="T_ID_GASTO"></asp:TextBox>
                                        </div>
                                        <div class="row clearfix">
                                            <div class="col-sm-4 form-control-label">
                                                <label class="font-bold"><strong>Tipo de Gasto</strong></label>
                                            </div>
                                            <div class="col-sm-8">
                                                <div class="form-group">
                                                    <asp:DropDownList ID="CB_GASTO_TIPO" runat="server" CssClass="form-control combopro">
                                                        <asp:ListItem Text="-- seleccione --" Value="0" Selected="True"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row clearfix">
                                            <div class="col-sm-4 form-control-label">
                                                <label class="font-bold"><strong>Detalle</strong></label>
                                            </div>
                                            <div class="col-sm-8">
                                                <div class="form-group">
                                                    <asp:TextBox runat="server" ID="T_GASTO_DETALLE" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row clearfix">
                                            <div class="col-sm-4 form-control-label">
                                                <label class="font-bold"><strong>Valor</strong></label>
                                            </div>
                                            <div class="col-sm-8">
                                                <div class="form-group">
                                                    <asp:TextBox runat="server" TextMode="Number" ID="T_GASTO_VALOR" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                    <asp:LinkButton runat="server" ID="B_GUARDAR_GASTOSGRAL" ClientIDMode="AutoID" OnClick="B_GUARDAR_GASTOSGRAL_Click" CssClass="btn btn-primary btn-round waves-effect"><i class="fa fa-save"></i> GUARDAR</asp:LinkButton>
                    <button type="button" class="btn btn-danger btn-simple btn-round waves-effect" data-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CODIGO_JAVASCRIPT" runat="server">
    <script>
        $(document).ready(function () {
            DT_PRINCIPAL();
        });

        // *************************************+ DATATABLES
        function DT_PRINCIPAL() {
            $('#<%= G_PRINCIPAL.ClientID %>').DataTable({
                destroy: true,
                stateSave: true,
                dom: 'Bfrtip',
                buttons: [
                    'copy', 'print'
                ],
                "language": {
                    "url": "assets/Spanish.json"
                },
                columnDefs: [{
                    type: 'date-uk',
                    targets: [4, 5]
                }]

            });
        }

        function DT_COMBUSTIBLE() {
            $('#<%= G_COMBUSTIBLE.ClientID %>').DataTable({
                destroy: true,
                stateSave: true,
                dom: 'Bfrtip',
                buttons: [
                    'copy', 'print'
                ],
                "language": {
                    "url": "assets/Spanish.json"
                }
            });
        }

        function DT_GASTOGRAL() {
            $('#<%= G_GASTOGRAL.ClientID %>').DataTable({
                destroy: true,
                stateSave: true,
                dom: 'Bfrtip',
                buttons: [
                    'copy', 'print'
                ],
                "language": {
                    "url": "assets/Spanish.json"
                }
            });
        }

        function DT_OTZ() {
            $('#<%= G_OTZ.ClientID %>').DataTable({
                destroy: true,
                stateSave: true,
                dom: 'Bfrtip',
                buttons: [
                    'copy', 'print'
                ],
                "language": {
                    "url": "assets/Spanish.json"
                }
            });
        }

        // *************************************+  MODALES
        function CARGARCOMBUSTIBLE() {
            $('#MODAL_COMBUSTIBLE').click();
        }

        function GASTOGENERAL() {
            $('#MODAL_GASTO').click();
        }

        function OTZ() {
            $('#MODAL_OTZ').click();
        }

        function ComboPro() {
            $('.combopro').selectize();
        }

        function CerrarModalOTZ() {
            $('#CERRAR_MODAL_OTZ').click();
        }

        function ImprimeGT(id_gt) {
            if (id_gt > 13155) {
                var win = window.open('Imprime_gt_new.aspx?num_gt=' + id_gt, '_blank');
            }
            else {
                var win = window.open('Imprime_gt.aspx?num_gt=' + id_gt, '_blank');
            }
            win.focus();
        }

        function ImprimeGT2() {
            var id_gt = $('#<%= T_ID.ClientID %>').val();
            if (id_gt > 13155) {
                var win = window.open('Imprime_gt_new.aspx?num_gt=' + id_gt, '_blank');
            }
            else {
                var win = window.open('Imprime_gt.aspx?num_gt=' + id_gt, '_blank');
            }
            win.focus();
        }

        function ResumenGT(id_gt) {
            var win = window.open('Resumen_GT.aspx?num_gt=' + id_gt, '_blank');
            win.focus();
        }
    </script>
</asp:Content>
