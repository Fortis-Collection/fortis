FORTIS COLLECTION README

Thanks for installing Fortis! Lets get you started:

First, you need to setup your Fortis Model assembly.

TDS Users:
* Download the TDS T4 templates from here: https://github.com/Fortis-Collection/fortis/tree/master/TDS/Code%20Generation%20Templates
* Setup TDS to generate a code model: https://www.hhogdev.com/help/tds/propcodegen
* Get all the item templates from your Sitecore instance into your TDS project and generate the Fortis model

Unicorn Users:
* Install the Transitus.Rainbow nuget package
* Download the T4 templates https://github.com/Fortis-Collection/fortis.codegen.transitus.rainbow/tree/master/Source/Fortis.Model and setup as in this blog post: http://www.sitecorenutsbolts.net/2015/10/14/Fortis-Model-Generation-with-Unicorn-3/
* Right click Model.tt and click Run Custom Tool

Add the model assembly binary to the Fortis.config file:
    <model name="Fortis.Model" assembly="Fortis.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" />

Install the version of the Fortis presentation layer you need:

MVC: Fortis.Mvc
Web forms: Fortis.WebForms

If you find a bug or have any questions, please ask on sitecorechat.slack.com or add an issue/pull request on GitHub: https://github.com/Fortis-Collection/fortis

