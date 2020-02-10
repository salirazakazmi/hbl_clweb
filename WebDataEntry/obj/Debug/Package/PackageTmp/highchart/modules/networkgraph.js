/*
 Highcharts JS v7.1.3 (2019-08-14)

 Force directed graph module

 (c) 2010-2019 Torstein Honsi

 License: www.highcharts.com/license
*/
(function(k){"object"===typeof module&&module.exports?(k["default"]=k,module.exports=k):"function"===typeof define&&define.amd?define("highcharts/modules/networkgraph",["highcharts"],function(l){k(l);k.Highcharts=l;return k}):k("undefined"!==typeof Highcharts?Highcharts:void 0)})(function(k){function l(g,b,c,d){g.hasOwnProperty(b)||(g[b]=d.apply(null,c))}k=k?k._modules:{};l(k,"mixins/nodes.js",[k["parts/Globals.js"],k["parts/Utilities.js"]],function(g,b){var c=b.defined,d=g.pick,h=g.Point;g.NodesMixin=
{createNode:function(a){function e(a,e){return g.find(a,function(a){return a.id===e})}var f=e(this.nodes,a),b=this.pointClass;if(!f){var p=this.options.nodes&&e(this.options.nodes,a);f=(new b).init(this,g.extend({className:"highcharts-node",isNode:!0,id:a,y:1},p));f.linksTo=[];f.linksFrom=[];f.formatPrefix="node";f.name=f.name||f.options.id;f.mass=d(f.options.mass,f.options.marker&&f.options.marker.radius,this.options.marker&&this.options.marker.radius,4);f.getSum=function(){var a=0,e=0;f.linksTo.forEach(function(e){a+=
e.weight});f.linksFrom.forEach(function(a){e+=a.weight});return Math.max(a,e)};f.offset=function(a,e){for(var d=0,b=0;b<f[e].length;b++){if(f[e][b]===a)return d;d+=f[e][b].weight}};f.hasShape=function(){var a=0;f.linksTo.forEach(function(e){e.outgoing&&a++});return!f.linksTo.length||a!==f.linksTo.length};this.nodes.push(f)}return f},generatePoints:function(){var a=this.chart,e={};g.Series.prototype.generatePoints.call(this);this.nodes||(this.nodes=[]);this.colorCounter=0;this.nodes.forEach(function(a){a.linksFrom.length=
0;a.linksTo.length=0;a.level=void 0});this.points.forEach(function(f){c(f.from)&&(e[f.from]||(e[f.from]=this.createNode(f.from)),e[f.from].linksFrom.push(f),f.fromNode=e[f.from],a.styledMode?f.colorIndex=d(f.options.colorIndex,e[f.from].colorIndex):f.color=f.options.color||e[f.from].color);c(f.to)&&(e[f.to]||(e[f.to]=this.createNode(f.to)),e[f.to].linksTo.push(f),f.toNode=e[f.to]);f.name=f.name||f.id},this);this.nodeLookup=e},setData:function(){this.nodes&&(this.nodes.forEach(function(a){a.destroy()}),
this.nodes.length=0);g.Series.prototype.setData.apply(this,arguments)},destroy:function(){this.data=[].concat(this.points||[],this.nodes);return g.Series.prototype.destroy.apply(this,arguments)},setNodeState:function(a){var e=arguments,d=this.isNode?this.linksTo.concat(this.linksFrom):[this.fromNode,this.toNode];"select"!==a&&d.forEach(function(a){a.series&&(h.prototype.setState.apply(a,e),a.isNode||(a.fromNode.graphic&&h.prototype.setState.apply(a.fromNode,e),a.toNode.graphic&&h.prototype.setState.apply(a.toNode,
e)))});h.prototype.setState.apply(this,e)}}});l(k,"modules/networkgraph/integrations.js",[k["parts/Globals.js"]],function(g){g.networkgraphIntegrations={verlet:{attractiveForceFunction:function(b,c){return(c-b)/b},repulsiveForceFunction:function(b,c){return(c-b)/b*(c>b?1:0)},barycenter:function(){var b=this.options.gravitationalConstant,c=this.barycenter.xFactor,d=this.barycenter.yFactor;c=(c-(this.box.left+this.box.width)/2)*b;d=(d-(this.box.top+this.box.height)/2)*b;this.nodes.forEach(function(b){b.fixedPosition||
(b.plotX-=c/b.mass/b.degree,b.plotY-=d/b.mass/b.degree)})},repulsive:function(b,c,d){c=c*this.diffTemperature/b.mass/b.degree;b.fixedPosition||(b.plotX+=d.x*c,b.plotY+=d.y*c)},attractive:function(b,c,d){var h=b.getMass(),a=-d.x*c*this.diffTemperature;c=-d.y*c*this.diffTemperature;b.fromNode.fixedPosition||(b.fromNode.plotX-=a*h.fromNode/b.fromNode.degree,b.fromNode.plotY-=c*h.fromNode/b.fromNode.degree);b.toNode.fixedPosition||(b.toNode.plotX+=a*h.toNode/b.toNode.degree,b.toNode.plotY+=c*h.toNode/
b.toNode.degree)},integrate:function(b,c){var d=-b.options.friction,h=b.options.maxSpeed,a=(c.plotX+c.dispX-c.prevX)*d;d*=c.plotY+c.dispY-c.prevY;var e=Math.abs,f=e(a)/(a||1);e=e(d)/(d||1);a=f*Math.min(h,Math.abs(a));d=e*Math.min(h,Math.abs(d));c.prevX=c.plotX+c.dispX;c.prevY=c.plotY+c.dispY;c.plotX+=a;c.plotY+=d;c.temperature=b.vectorLength({x:a,y:d})},getK:function(b){return Math.pow(b.box.width*b.box.height/b.nodes.length,.5)}},euler:{attractiveForceFunction:function(b,c){return b*b/c},repulsiveForceFunction:function(b,
c){return c*c/b},barycenter:function(){var b=this.options.gravitationalConstant,c=this.barycenter.xFactor,d=this.barycenter.yFactor;this.nodes.forEach(function(h){if(!h.fixedPosition){var a=h.getDegree();a*=1+a/2;h.dispX+=(c-h.plotX)*b*a/h.degree;h.dispY+=(d-h.plotY)*b*a/h.degree}})},repulsive:function(b,c,d,h){b.dispX+=d.x/h*c/b.degree;b.dispY+=d.y/h*c/b.degree},attractive:function(b,c,d,h){var a=b.getMass(),e=d.x/h*c;c*=d.y/h;b.fromNode.fixedPosition||(b.fromNode.dispX-=e*a.fromNode/b.fromNode.degree,
b.fromNode.dispY-=c*a.fromNode/b.fromNode.degree);b.toNode.fixedPosition||(b.toNode.dispX+=e*a.toNode/b.toNode.degree,b.toNode.dispY+=c*a.toNode/b.toNode.degree)},integrate:function(b,c){c.dispX+=c.dispX*b.options.friction;c.dispY+=c.dispY*b.options.friction;var d=c.temperature=b.vectorLength({x:c.dispX,y:c.dispY});0!==d&&(c.plotX+=c.dispX/d*Math.min(Math.abs(c.dispX),b.temperature),c.plotY+=c.dispY/d*Math.min(Math.abs(c.dispY),b.temperature))},getK:function(b){return Math.pow(b.box.width*b.box.height/
b.nodes.length,.3)}}}});l(k,"modules/networkgraph/QuadTree.js",[k["parts/Globals.js"]],function(g){var b=g.QuadTreeNode=function(d){this.box=d;this.boxSize=Math.min(d.width,d.height);this.nodes=[];this.body=this.isInternal=!1;this.isEmpty=!0};g.extend(b.prototype,{insert:function(d,c){this.isInternal?this.nodes[this.getBoxPosition(d)].insert(d,c-1):(this.isEmpty=!1,this.body?c?(this.isInternal=!0,this.divideBox(),!0!==this.body&&(this.nodes[this.getBoxPosition(this.body)].insert(this.body,c-1),this.body=
!0),this.nodes[this.getBoxPosition(d)].insert(d,c-1)):(c=new b({top:d.plotX,left:d.plotY,width:.1,height:.1}),c.body=d,c.isInternal=!1,this.nodes.push(c)):(this.isInternal=!1,this.body=d))},updateMassAndCenter:function(){var d=0,b=0,a=0;this.isInternal?(this.nodes.forEach(function(e){e.isEmpty||(d+=e.mass,b+=e.plotX*e.mass,a+=e.plotY*e.mass)}),b/=d,a/=d):this.body&&(d=this.body.mass,b=this.body.plotX,a=this.body.plotY);this.mass=d;this.plotX=b;this.plotY=a},divideBox:function(){var d=this.box.width/
2,c=this.box.height/2;this.nodes[0]=new b({left:this.box.left,top:this.box.top,width:d,height:c});this.nodes[1]=new b({left:this.box.left+d,top:this.box.top,width:d,height:c});this.nodes[2]=new b({left:this.box.left+d,top:this.box.top+c,width:d,height:c});this.nodes[3]=new b({left:this.box.left,top:this.box.top+c,width:d,height:c})},getBoxPosition:function(b){var d=b.plotY<this.box.top+this.box.height/2;return b.plotX<this.box.left+this.box.width/2?d?0:3:d?1:2}});var c=g.QuadTree=function(d,c,a,e){this.box=
{left:d,top:c,width:a,height:e};this.maxDepth=25;this.root=new b(this.box,"0");this.root.isInternal=!0;this.root.isRoot=!0;this.root.divideBox()};g.extend(c.prototype,{insertNodes:function(b){b.forEach(function(b){this.root.insert(b,this.maxDepth)},this)},visitNodeRecursive:function(b,c,a){var e;b||(b=this.root);b===this.root&&c&&(e=c(b));!1!==e&&(b.nodes.forEach(function(b){if(b.isInternal){c&&(e=c(b));if(!1===e)return;this.visitNodeRecursive(b,c,a)}else b.body&&c&&c(b.body);a&&a(b)},this),b===this.root&&
a&&a(b))},calculateMassAndCenter:function(){this.visitNodeRecursive(null,null,function(b){b.updateMassAndCenter()})}})});l(k,"modules/networkgraph/layouts.js",[k["parts/Globals.js"],k["parts/Utilities.js"]],function(g,b){var c=b.defined,d=g.pick;b=g.addEvent;var h=g.Chart;g.layouts={"reingold-fruchterman":function(){}};g.extend(g.layouts["reingold-fruchterman"].prototype,{init:function(a){this.options=a;this.nodes=[];this.links=[];this.series=[];this.box={x:0,y:0,width:0,height:0};this.setInitialRendering(!0);
this.integration=g.networkgraphIntegrations[a.integration];this.attractiveForce=d(a.attractiveForce,this.integration.attractiveForceFunction);this.repulsiveForce=d(a.repulsiveForce,this.integration.repulsiveForceFunction);this.approximation=a.approximation},start:function(){var a=this.series,b=this.options;this.currentStep=0;this.forces=a[0]&&a[0].forces||[];this.initialRendering&&(this.initPositions(),a.forEach(function(a){a.render()}));this.setK();this.resetSimulation(b);b.enableSimulation&&this.step()},
step:function(){var a=this,b=this.series,c=this.options;a.currentStep++;"barnes-hut"===a.approximation&&(a.createQuadTree(),a.quadTree.calculateMassAndCenter());a.forces.forEach(function(b){a[b+"Forces"](a.temperature)});a.applyLimits(a.temperature);a.temperature=a.coolDown(a.startTemperature,a.diffTemperature,a.currentStep);a.prevSystemTemperature=a.systemTemperature;a.systemTemperature=a.getSystemTemperature();c.enableSimulation&&(b.forEach(function(a){a.chart&&a.render()}),a.maxIterations--&&isFinite(a.temperature)&&
!a.isStable()?(a.simulation&&g.win.cancelAnimationFrame(a.simulation),a.simulation=g.win.requestAnimationFrame(function(){a.step()})):a.simulation=!1)},stop:function(){this.simulation&&g.win.cancelAnimationFrame(this.simulation)},setArea:function(a,b,c,d){this.box={left:a,top:b,width:c,height:d}},setK:function(){this.k=this.options.linkLength||this.integration.getK(this)},addNodes:function(a){a.forEach(function(a){-1===this.nodes.indexOf(a)&&this.nodes.push(a)},this)},removeNode:function(a){a=this.nodes.indexOf(a);
-1!==a&&this.nodes.splice(a,1)},removeLink:function(a){a=this.links.indexOf(a);-1!==a&&this.links.splice(a,1)},addLinks:function(a){a.forEach(function(a){-1===this.links.indexOf(a)&&this.links.push(a)},this)},addSeries:function(a){-1===this.series.indexOf(a)&&this.series.push(a)},clear:function(){this.nodes.length=0;this.links.length=0;this.series.length=0;this.resetSimulation()},resetSimulation:function(){this.forcedStop=!1;this.systemTemperature=0;this.setMaxIterations();this.setTemperature();this.setDiffTemperature()},
setMaxIterations:function(a){this.maxIterations=d(a,this.options.maxIterations)},setTemperature:function(){this.temperature=this.startTemperature=Math.sqrt(this.nodes.length)},setDiffTemperature:function(){this.diffTemperature=this.startTemperature/(this.options.maxIterations+1)},setInitialRendering:function(a){this.initialRendering=a},createQuadTree:function(){this.quadTree=new g.QuadTree(this.box.left,this.box.top,this.box.width,this.box.height);this.quadTree.insertNodes(this.nodes)},initPositions:function(){var a=
this.options.initialPositions;g.isFunction(a)?(a.call(this),this.nodes.forEach(function(a){c(a.prevX)||(a.prevX=a.plotX);c(a.prevY)||(a.prevY=a.plotY);a.dispX=0;a.dispY=0})):"circle"===a?this.setCircularPositions():this.setRandomPositions()},setCircularPositions:function(){function a(b){b.linksFrom.forEach(function(b){g[b.toNode.id]||(g[b.toNode.id]=!0,m.push(b.toNode),a(b.toNode))})}var b=this.box,c=this.nodes,n=2*Math.PI/(c.length+1),p=c.filter(function(a){return 0===a.linksTo.length}),m=[],g={},
h=this.options.initialPositionRadius;p.forEach(function(b){m.push(b);a(b)});m.length?c.forEach(function(a){-1===m.indexOf(a)&&m.push(a)}):m=c;m.forEach(function(a,c){a.plotX=a.prevX=d(a.plotX,b.width/2+h*Math.cos(c*n));a.plotY=a.prevY=d(a.plotY,b.height/2+h*Math.sin(c*n));a.dispX=0;a.dispY=0})},setRandomPositions:function(){function a(a){a=a*a/Math.PI;return a-=Math.floor(a)}var b=this.box,c=this.nodes,n=c.length+1;c.forEach(function(c,e){c.plotX=c.prevX=d(c.plotX,b.width*a(e));c.plotY=c.prevY=d(c.plotY,
b.height*a(n+e));c.dispX=0;c.dispY=0})},force:function(a){this.integration[a].apply(this,Array.prototype.slice.call(arguments,1))},barycenterForces:function(){this.getBarycenter();this.force("barycenter")},getBarycenter:function(){var a=0,b=0,c=0;this.nodes.forEach(function(d){b+=d.plotX*d.mass;c+=d.plotY*d.mass;a+=d.mass});return this.barycenter={x:b,y:c,xFactor:b/a,yFactor:c/a}},barnesHutApproximation:function(a,b){var c=this.getDistXY(a,b),d=this.vectorLength(c);if(a!==b&&0!==d)if(b.isInternal)if(b.boxSize/
d<this.options.theta&&0!==d){var e=this.repulsiveForce(d,this.k);this.force("repulsive",a,e*b.mass,c,d);var m=!1}else m=!0;else e=this.repulsiveForce(d,this.k),this.force("repulsive",a,e*b.mass,c,d);return m},repulsiveForces:function(){var a=this;"barnes-hut"===a.approximation?a.nodes.forEach(function(b){a.quadTree.visitNodeRecursive(null,function(c){return a.barnesHutApproximation(b,c)})}):a.nodes.forEach(function(b){a.nodes.forEach(function(c){if(b!==c&&!b.fixedPosition){var d=a.getDistXY(b,c);
var e=a.vectorLength(d);if(0!==e){var f=a.repulsiveForce(e,a.k);a.force("repulsive",b,f*c.mass,d,e)}}})})},attractiveForces:function(){var a=this,b,c,d;a.links.forEach(function(e){e.fromNode&&e.toNode&&(b=a.getDistXY(e.fromNode,e.toNode),c=a.vectorLength(b),0!==c&&(d=a.attractiveForce(c,a.k),a.force("attractive",e,d,b,c)))})},applyLimits:function(){var a=this;a.nodes.forEach(function(b){b.fixedPosition||(a.integration.integrate(a,b),a.applyLimitBox(b,a.box),b.dispX=0,b.dispY=0)})},applyLimitBox:function(a,
b){var c=a.radius;a.plotX=Math.max(Math.min(a.plotX,b.width-c),b.left+c);a.plotY=Math.max(Math.min(a.plotY,b.height-c),b.top+c)},coolDown:function(a,b,c){return a-b*c},isStable:function(){return.00001>Math.abs(this.systemTemperature-this.prevSystemTemperature)||0>=this.temperature},getSystemTemperature:function(){return this.nodes.reduce(function(a,b){return a+b.temperature},0)},vectorLength:function(a){return Math.sqrt(a.x*a.x+a.y*a.y)},getDistR:function(a,b){a=this.getDistXY(a,b);return this.vectorLength(a)},
getDistXY:function(a,b){var c=a.plotX-b.plotX;a=a.plotY-b.plotY;return{x:c,y:a,absX:Math.abs(c),absY:Math.abs(a)}}});b(h,"predraw",function(){this.graphLayoutsLookup&&this.graphLayoutsLookup.forEach(function(a){a.stop()})});b(h,"render",function(){function a(a){a.maxIterations--&&isFinite(a.temperature)&&!a.isStable()&&!a.options.enableSimulation&&(a.beforeStep&&a.beforeStep(),a.step(),c=!1,b=!0)}var b=!1;if(this.graphLayoutsLookup){g.setAnimation(!1,this);for(this.graphLayoutsLookup.forEach(function(a){a.start()});!c;){var c=
!0;this.graphLayoutsLookup.forEach(a)}b&&this.series.forEach(function(a){a&&a.layout&&a.render()})}})});l(k,"modules/networkgraph/draggable-nodes.js",[k["parts/Globals.js"]],function(g){var b=g.Chart,c=g.addEvent;g.dragNodesMixin={onMouseDown:function(b,c){c=this.chart.pointer.normalize(c);b.fixedPosition={chartX:c.chartX,chartY:c.chartY,plotX:b.plotX,plotY:b.plotY};b.inDragMode=!0},onMouseMove:function(b,c){if(b.fixedPosition&&b.inDragMode){var a=this.chart,d=a.pointer.normalize(c);c=b.fixedPosition.chartX-
d.chartX;d=b.fixedPosition.chartY-d.chartY;if(5<Math.abs(c)||5<Math.abs(d))c=b.fixedPosition.plotX-c,d=b.fixedPosition.plotY-d,a.isInsidePlot(c,d)&&(b.plotX=c,b.plotY=d,b.hasDragged=!0,this.redrawHalo(b),this.layout.simulation?this.layout.resetSimulation():(this.layout.setInitialRendering(!1),this.layout.enableSimulation?this.layout.start():this.layout.setMaxIterations(1),this.chart.redraw(),this.layout.setInitialRendering(!0)))}},onMouseUp:function(b,c){b.fixedPosition&&b.hasDragged&&(this.layout.enableSimulation?
this.layout.start():this.chart.redraw(),b.inDragMode=b.hasDragged=!1,this.options.fixedDraggable||delete b.fixedPosition)},redrawHalo:function(b){b&&this.halo&&this.halo.attr({d:b.haloPath(this.options.states.hover.halo.size)})}};c(b,"load",function(){var b=this,g,a,e;b.container&&(g=c(b.container,"mousedown",function(d){var f=b.hoverPoint;f&&f.series&&f.series.hasDraggableNodes&&f.series.options.draggable&&(f.series.onMouseDown(f,d),a=c(b.container,"mousemove",function(a){return f&&f.series&&f.series.onMouseMove(f,
a)}),e=c(b.container.ownerDocument,"mouseup",function(b){a();e();return f&&f.series&&f.series.onMouseUp(f,b)}))}));c(b,"destroy",function(){g()})})});l(k,"modules/networkgraph/networkgraph.src.js",[k["parts/Globals.js"],k["parts/Utilities.js"]],function(g,b){var c=b.defined,d=g.addEvent;b=g.seriesType;var h=g.seriesTypes,a=g.pick,e=g.Point,f=g.Series,k=g.dragNodesMixin;b("networkgraph","line",{stickyTracking:!1,inactiveOtherPoints:!0,marker:{enabled:!0,states:{inactive:{opacity:.3,animation:{duration:50}}}},
states:{inactive:{linkOpacity:.3,animation:{duration:50}}},dataLabels:{formatter:function(){return this.key},linkFormatter:function(){return this.point.fromNode.name+"<br>"+this.point.toNode.name},linkTextPath:{enabled:!0},textPath:{enabled:!1}},link:{color:"rgba(100, 100, 100, 0.5)",width:1},draggable:!0,layoutAlgorithm:{initialPositions:"circle",initialPositionRadius:1,enableSimulation:!1,theta:.5,maxSpeed:10,approximation:"none",type:"reingold-fruchterman",integration:"euler",maxIterations:1E3,
gravitationalConstant:.0625,friction:-.981},showInLegend:!1},{forces:["barycenter","repulsive","attractive"],hasDraggableNodes:!0,drawGraph:null,isCartesian:!1,requireSorting:!1,directTouch:!0,noSharedTooltip:!0,pointArrayMap:["from","to"],trackerGroups:["group","markerGroup","dataLabelsGroup"],drawTracker:g.TrackerMixin.drawTrackerPoint,animate:null,buildKDTree:g.noop,createNode:g.NodesMixin.createNode,destroy:g.NodesMixin.destroy,init:function(){f.prototype.init.apply(this,arguments);d(this,"updatedData",
function(){this.layout&&this.layout.stop()});return this},generatePoints:function(){var b;g.NodesMixin.generatePoints.apply(this,arguments);this.options.nodes&&this.options.nodes.forEach(function(a){this.nodeLookup[a.id]||(this.nodeLookup[a.id]=this.createNode(a.id))},this);for(b=this.nodes.length-1;0<=b;b--){var c=this.nodes[b];c.degree=c.getDegree();c.radius=a(c.marker&&c.marker.radius,this.options.marker&&this.options.marker.radius,0);this.nodeLookup[c.id]||c.remove()}this.data.forEach(function(a){a.formatPrefix=
"link"});this.indexateNodes()},indexateNodes:function(){this.nodes.forEach(function(a,b){a.index=b})},markerAttribs:function(a,b){b=f.prototype.markerAttribs.call(this,a,b);c(a.plotY)||(b.y=0);b.x=(a.plotX||0)-(b.width/2||0);return b},translate:function(){this.processedXData||this.processData();this.generatePoints();this.deferLayout();this.nodes.forEach(function(a){a.isInside=!0;a.linksFrom.forEach(function(a){a.shapeType="path";a.y=1})})},deferLayout:function(){var a=this.options.layoutAlgorithm,
b=this.chart.graphLayoutsStorage,d=this.chart.graphLayoutsLookup,f=this.chart.options.chart;if(this.visible){b||(this.chart.graphLayoutsStorage=b={},this.chart.graphLayoutsLookup=d=[]);var e=b[a.type];e||(a.enableSimulation=c(f.forExport)?!f.forExport:a.enableSimulation,b[a.type]=e=new g.layouts[a.type],e.init(a),d.splice(e.index,0,e));this.layout=e;e.setArea(0,0,this.chart.plotWidth,this.chart.plotHeight);e.addSeries(this);e.addNodes(this.nodes);e.addLinks(this.points)}},render:function(){var a=
this.points,b=this.chart.hoverPoint,c=[];this.points=this.nodes;h.line.prototype.render.call(this);this.points=a;a.forEach(function(a){a.fromNode&&a.toNode&&(a.renderLink(),a.redrawLink())});b&&b.series===this&&this.redrawHalo(b);this.chart.hasRendered&&!this.options.dataLabels.allowOverlap&&(this.nodes.concat(this.points).forEach(function(a){a.dataLabel&&c.push(a.dataLabel)}),this.chart.hideOverlappingLabels(c))},drawDataLabels:function(){var a=this.options.dataLabels.textPath;f.prototype.drawDataLabels.apply(this,
arguments);this.points=this.data;this.options.dataLabels.textPath=this.options.dataLabels.linkTextPath;f.prototype.drawDataLabels.apply(this,arguments);this.points=this.nodes;this.options.dataLabels.textPath=a},pointAttribs:function(b,c){var d=c||b.state||"normal";c=f.prototype.pointAttribs.call(this,b,d);d=this.options.states[d];b.isNode||(c=b.getLinkAttributes(),d&&(c={stroke:d.linkColor||c.stroke,dashstyle:d.linkDashStyle||c.dashstyle,opacity:a(d.linkOpacity,c.opacity),"stroke-width":d.linkColor||
c["stroke-width"]}));return c},redrawHalo:k.redrawHalo,onMouseDown:k.onMouseDown,onMouseMove:k.onMouseMove,onMouseUp:k.onMouseUp,setState:function(a,b){b?(this.points=this.nodes.concat(this.data),f.prototype.setState.apply(this,arguments),this.points=this.data):f.prototype.setState.apply(this,arguments);this.layout.simulation||a||this.render()}},{setState:g.NodesMixin.setNodeState,init:function(){e.prototype.init.apply(this,arguments);this.series.options.draggable&&!this.series.chart.styledMode&&
(d(this,"mouseOver",function(){g.css(this.series.chart.container,{cursor:"move"})}),d(this,"mouseOut",function(){g.css(this.series.chart.container,{cursor:"default"})}));return this},getDegree:function(){var a=this.isNode?this.linksFrom.length+this.linksTo.length:0;return 0===a?1:a},getLinkAttributes:function(){var b=this.series.options.link,c=this.options;return{"stroke-width":a(c.width,b.width),stroke:c.color||b.color,dashstyle:c.dashStyle||b.dashStyle,opacity:a(c.opacity,b.opacity,1)}},renderLink:function(){if(!this.graphic&&
(this.graphic=this.series.chart.renderer.path(this.getLinkPath()).add(this.series.group),!this.series.chart.styledMode)){var a=this.series.pointAttribs(this);this.graphic.attr(a);(this.dataLabels||[]).forEach(function(b){b&&b.attr({opacity:a.opacity})})}},redrawLink:function(){var a=this.getLinkPath();if(this.graphic){this.shapeArgs={d:a};if(!this.series.chart.styledMode){var b=this.series.pointAttribs(this);this.graphic.attr(b);(this.dataLabels||[]).forEach(function(a){a&&a.attr({opacity:b.opacity})})}this.graphic.animate(this.shapeArgs);
this.plotX=(a[1]+a[4])/2;this.plotY=(a[2]+a[5])/2}},getMass:function(){var a=this.fromNode.mass,b=this.toNode.mass,c=a+b;return{fromNode:1-a/c,toNode:1-b/c}},getLinkPath:function(){var a=this.fromNode,b=this.toNode;a.plotX>b.plotX&&(a=this.toNode,b=this.fromNode);return["M",a.plotX,a.plotY,"L",b.plotX,b.plotY]},isValid:function(){return!this.isNode||c(this.id)},remove:function(a,b){var c=this.series,d=c.options.nodes||[],e,g=d.length;if(this.isNode){c.points=[];[].concat(this.linksFrom).concat(this.linksTo).forEach(function(a){e=
a.fromNode.linksFrom.indexOf(a);-1<e&&a.fromNode.linksFrom.splice(e,1);e=a.toNode.linksTo.indexOf(a);-1<e&&a.toNode.linksTo.splice(e,1);f.prototype.removePoint.call(c,c.data.indexOf(a),!1,!1)});c.points=c.data.slice();for(c.nodes.splice(c.nodes.indexOf(this),1);g--;)if(d[g].id===this.options.id){c.options.nodes.splice(g,1);break}this&&this.destroy();c.isDirty=!0;c.isDirtyData=!0;a&&c.chart.redraw(a)}else c.removePoint(c.data.indexOf(this),a,b)},destroy:function(){this.isNode?(this.linksFrom.concat(this.linksTo).forEach(function(a){a.destroyElements&&
a.destroyElements()}),this.series.layout.removeNode(this)):this.series.layout.removeLink(this);return e.prototype.destroy.apply(this,arguments)}});""});l(k,"masters/modules/networkgraph.src.js",[],function(){})});
//# sourceMappingURL=networkgraph.js.map