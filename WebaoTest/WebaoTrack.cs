﻿using System.Collections.Generic;
using Webao.Attributes;
using Webao.Test.Dto;

namespace Webao.Test
{
[BaseUrl("http://ws.audioscrobbler.com/2.0/")]
[AddParameterAttribute("format", "json")]
[AddParameterAttribute("api_key", "26915fe20db044f69f7214479af36dc6")]

public class WebaoTrack : AbstractAccessObject
{
    public WebaoTrack(IRequest req) : base(req) { }

    [Get("?method=geo.gettoptracks&country={country}")]
    [Mapping(typeof(DtoGeoTopTracks), ".Tracks.Track")]
    public List<Track> GeoGetTopTracks(string country) {
        return (List<Track>) Request(country);
    }
}
}