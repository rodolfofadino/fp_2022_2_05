﻿using CodeHollow.FeedReader;
using fiap2022.core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fiap2022.core.Services
{
    public class NoticiaService
    {

        public NoticiaService()
        {

        }

        public List<Noticia> Load(int totalDeNoticias)
        {
            var noticias = new List<Noticia>();
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

            return noticias.Where(a=>a.Imagem != "").ToList();
        }

    }
}