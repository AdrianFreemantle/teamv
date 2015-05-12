﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MobilePoll.Environment;
using MobilePoll.Infrastructure.Ioc;
using MobilePoll.Infrastructure.Serialization;
using MobilePoll.Serialization;
using Shouldly;

namespace MobilePoll.Infrastructure.Tests
{
    public class Dto
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
    }

    [TestClass]
    public class SerializerTests
    {
        static SerializerTests()
        {
            Configuration.Initialize(new AutofacAdapter());
        }

        [TestMethod]
        public void Serializer_can_serialize_to_and_deserialize_from_a_string()
        {
            ISerializer serializer = Configuration.RootContainer.GetInstance<ISerializer>();

            Dto dto = new Dto
            {
                Id = Guid.NewGuid(),
                Date = DateTime.Today
            };

            string serializedDto = serializer.Serialize(dto);
            Dto dtoCopy = serializer.Deserialize<Dto>(serializedDto);

            dto.Id.ShouldBe(dtoCopy.Id);
            dto.Date.ShouldBe(dtoCopy.Date);
        }

        [TestMethod]
        public void Serializer_can_serialize_to_and_deserialize_from_a_byte_array()
        {
            ISerializer serializer = Configuration.RootContainer.GetInstance<ISerializer>();

            Dto dto = new Dto
            {
                Id = Guid.NewGuid(),
                Date = DateTime.Today
            };

            var serializedDto = serializer.ToByteArray(dto);
            var dtoCopy = serializer.FromByteArray<Dto>(serializedDto);

            dto.Id.ShouldBe(dtoCopy.Id);
            dto.Date.ShouldBe(dtoCopy.Date);
        }
    }
}