# BookSearchAPI
Search the Google Books API

I wanted to learn how to parse JSON, so I built this console application. It searches Google Books and returns results in a nice list that can be used in a variety of ways. I'm just displaying the list to the user. 

Some nice features include the JSON parsing via the great NewtonSoft.JSON library. I also learnt a nice way of displaying all the properties of an object.

To get it to run, you'll need to get your own Google Books API key. This is easy to get, just Google how to get it. Once you have the key, edit Program.cs, and add your key in line 85. Should be obvious what you need to add. 

<code>
string GoogleBooksApiKey = "Your Google Books API Key here"
</code>

The code is well commented throughout so if you're a beginner, you should be able to see what's going on.
