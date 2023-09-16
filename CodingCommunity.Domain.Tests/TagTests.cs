using CodingCommunity.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingCommunity.Domain.Tests
{
    public class TagTests
    {
        [Fact]
        public void CreateTag_Success()
        {

            Tag tag = new Tag
            {
                TagID = 1,
                TagName = "C#"
            };

            Assert.Equal(1, tag.TagID);
            Assert.Equal("C#", tag.TagName);
        }
    }
}
