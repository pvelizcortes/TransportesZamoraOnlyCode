$(function () {
    //Basic Map
    var basicMap = new GMaps({
        el: '#gmap_basic_example',
        lat: -12.043333,
        lng: -77.028333
    });

    //Markers
    var markers = new GMaps({
        div: '#gmap_markers',
        lat: -12.043333,
        lng: -77.028333
    });
    markers.addMarker({
        lat: -12.043333,
        lng: -77.03,
        title: 'Lima',
        details: {
            database_id: 42,
            author: 'HPNeo'
        },
        click: function (e) {
            if (console.log)
                console.log(e);
            alert('You clicked in this marker');
        }
    });
    markers.addMarker({
        lat: -12.042,
        lng: -77.028333,
        title: 'Marker with InfoWindow',
        infoWindow: {
            content: '<p>HTML Content</p>'
        }
    });

    //Panorama
    var panorama = GMaps.createPanorama({
        el: '#gmap_panorama',
        lat: 42.3455,
        lng: -71.0983
    });

});