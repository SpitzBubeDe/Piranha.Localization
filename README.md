# Piranha.Localization

Localization module for Piranha CMS to support multiple languages.

## License

The module is released under the **MIT** license.

## Status

The localization module is under development. Check the issue list for information about the current status. The module is built for the **currently released** version of Piranha CMS, which is **2.2.1** and will not run together with the development master branch of the Piranha repository.

## Configuration

After referencing the module project in your application you have to add the languages you want in the startup code for your application, i.e **Global.asax** for MVC applications, and **_AppStart.cshtml** for Web Pages.

For example, to add **Swedish** as a secondary language, the following code should be added:

    Piranha.Localization.Module.RegisterLanguage(
      new Piranha.Localization.Language() {
        Culture = "sv-SE",
        Name = "Swedish",
        UrlPrefix = "sv"
      });

The above statement will both register the language in the localization **as well** as adding the culture to be handled by the client application.

## How to access the translations

Translated content is access by adding the specified **url prefix** to the current url. For example, given that you have the following permalink:

    http://mysite.com/about-me

The swedish translation for the page will be available at:

    http://mysite.com/sv/about-me
 
     