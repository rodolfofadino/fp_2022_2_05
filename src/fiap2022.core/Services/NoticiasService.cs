using CodeHollow.FeedReader;
using fiap2022.core.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fiap2022.core.Services
{
    public class NoticiaService
    {
        private IMemoryCache _cache;

        public NoticiaService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public List<Noticia> Load(int totalDeNoticias, string categoria = "padrao")
        {
            var noticias = new List<Noticia>();
            var key = $"noticias_{categoria}";

            if (!_cache.TryGetValue(key, out noticias))
            {
                noticias = new List<Noticia>();
                var feed = FeedReader.ReadAsync("https://g1.globo.com/rss/g1/turismo-e-viagem/").Result;

                foreach (var item in feed.Items)
                {
                    var feedItem = item.SpecificItem as CodeHollow.FeedReader.Feeds.MediaRssFeedItem;
                    var media = feedItem.Media;
                    var url = "";
                    if (media.Any())
                        url = media.FirstOrDefault().Url;
                    noticias.Add(new Noticia() { Id = 1, Titulo = item.Title, Link = item.Link, Imagem = url });
                }

                var cacheEntryOption = new MemoryCacheEntryOptions()
                    //.SetSlidingExpiration()
                    .SetAbsoluteExpiration(DateTime.Now.AddSeconds(60));

                _cache.Set(key, noticias, cacheEntryOption);
            }

            return noticias.Where(a => a.Imagem != "").ToList();
        }

    }
}