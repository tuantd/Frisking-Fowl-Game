Scarlet Post Processor
======================

Unity plugins frequently require adding frameworks to an Xcode project. For example, [Diffusion](http://scarlet.io/docs/diffusion.html) requires adding `Social.framework` or Xcode will throw linker errors. The common trend is for each plugin to create its own PostProcessBuildPlayer script. At best, this results in several scripts doing basically the same thing. At worst, these scripts conflict and cause errors. 

Rather than attempt to add frameworks for our plugins directly, we've opted to use Xcode 5's `@import` feature. This will automatically add the required frameworks on it's own. That way, we can use a single script for all of our plugins. In fact, in theory, this could fix errors with other plugins as well! `@import` will [automatically convert](http://stackoverflow.com/questions/18947516/import-vs-import-ios-7) `#import` statements, which means that enabling modules include missing frameworks even for files that don't use @import. 


Usage
-----

Simply include the `ScarletPostProcessor.cs` script in an `Editor` folder of your project. This will enable `CLANG_ENABLE_MODULES` for your output Xcode project.

To take advantage of this in your plugins, you can use `@import` to include necessary frameworks:

```css
@import Social
@import Accelerate

...
```
