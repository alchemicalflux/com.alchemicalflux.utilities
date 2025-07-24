# [1.4.0](https://github.com/alchemicalflux/com.alchemicalflux.utilities/compare/v1.3.1...v1.4.0) (2025-07-24)


### Bug Fixes

* Add granulated options for NullCheck menu items and add message when no errors occur ([b601f57](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/b601f57e950beae59c5f0efdae41b868871ff42a))
* Add NaN checks to Vector2/3 interpolators ([60d645b](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/60d645b7fd394ac58955ba2366d2d9b1fd322a41))
* Add NaN progress errors to quaterion interpolators ([4c9b867](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/4c9b86718cfdf98686f17afb610db93c8b4c2c59))
* Add null safety checks to callbacks ([2d1d41a](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/2d1d41a965c681b20d1ab41df78b622e9d4b52bd))
* Add NullCheck locating and validation functionality, menu items, and on load protocols ([0cbfe5d](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/0cbfe5d610ff7da1d2c28d90ae3df952336ece81))
* Add reflection utilities for finding fields on GameObjects with a specific attribute. ([a821505](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/a8215056caf4e315152bacb5ce5efe9e503a73d8))
* Adjust ApplyProgress return for start/end eval ([54e1f08](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/54e1f08b6168bb93d49b5df84fec1f9ede5322ff))
* Adjust behavior when dealing with an empty list. ([a5b29e2](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/a5b29e20f95475cdde8beeffa6ee3017a33df328))
* Adjust child processing check for Unity objects ([5e663ef](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/5e663ef0f482631bd2bcbb7be767005f35f885a4))
* Adjust parameters to correct type ([56b6ee7](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/56b6ee71fc6c6c3b2db55228367dd0d30d0eac03))
* Convert CachNode to class ([d4a09cc](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/d4a09ccbcd7f2d600ae9909c8fb70f0e83f2b7c9))
* Convert list of Action to single Action ([be16faa](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/be16faac51e508cfbd31cfdc047ec600263d6867))
* Correct exceptions and code consistency ([52c0323](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/52c0323ccb449b019087037595c35c062423a726))
* Corrected attribute processing for non-primitive fields ([c0d01e5](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/c0d01e5ca1ade4b7f7c5b1049456443d89c22ea0))
* Covert Quaternion to De Casteljau curve ([600a9c1](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/600a9c163e8138d7ae30988133bdf1b7f98ded59))
* Ensure value fields still display as violation when included in prefabs ([60352fc](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/60352fc198ce32332802649f676fad66ea37203e))
* Extend assembly definitions for testing ([6b1c0bc](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/6b1c0bc5cf551bafd6d9783955e252fdd5c1a024))
* Improve constructor initialization ([c544261](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/c544261cc880c487df6c7222e76eb7fca2568e39))
* Improve validation and event handling ([4f0f76b](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/4f0f76bed927e36355ff824b5a98797bd810299e))
* Limit Bezier curves to 0-1 range ([3724e6f](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/3724e6f94f08c705ca6c3ab0d9d4929260b3be8f))
* Properly label IUnityEnumFuncMapDrawerBase interface ([9f31c71](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/9f31c71e1a384ab28860cbcbd24824b2abf51ebc))
* Provide default delegate when map not initialized ([90493c1](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/90493c1d26d571ded9635bfc26755c5ceb1693c2))
* Remove debug output from Singleton ([8e7e97d](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/8e7e97de1082b7111f2c314054d73f594421e5bc))
* Rename for better representation ([beb17c9](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/beb17c974bea33587a26c50e3ea3f1adcd90acf6))
* Reorder NaN and equality checks for approximations ([bde496f](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/bde496f6fadc27107de6f1144e04244635d361c4))
* Uncomment accidentally removed Singleton calls ([de15bcb](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/de15bcb867329acfb8ff008aba6bd9331372df5a))
* Update DeCasteljau curve to iterative ([d270d4e](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/d270d4ec41577874f13f4f8d03b01f3bc11d8153))
* Wrap custom approximate with Debug/NUnit checks ([c51cddf](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/c51cddf562cf52b8f4d23e4e4e9e72e67d7d551f))


### Features

* Add Bezier curve interpolator base class ([548d444](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/548d4449e7eb0c1c85f207ab55eaacbf11e8c180))
* Add Color extension for luminance ([24fea1f](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/24fea1f6050c33991b93f49e042cc336e142ae89))
* Add constructors to UnityEnumFuncMap ([44f33a9](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/44f33a99932d6f4a220baba538a12af5426b3f57))
* Add De Casteljau bezier curve interpolator ([8b9794d](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/8b9794de36f6795f02bbfd35baa00c60c8ab8ed8))
* Add functionality for Color class ([e42b2cc](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/e42b2cc2d4686257a73572adbfe74e848476dab7))
* Add index return, discard, and shuffle functions ([0cd560b](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/0cd560b741d098ee466391cb87b0c391c7938580))
* Add informative properties and functions ([6972072](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/6972072f33e12f1321e4a5db6cd863f4d786d5c5))
* Add initial WaitManager class ([3cff1ca](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/3cff1caead5c16fd062f19ab4129f2f21dc63825))
* Add InterpolatorBase to handle common functionality ([7e620a4](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/7e620a4c2576a5d1aaf5b15e41d105c3ad25ddce))
* Add IOnUpdateEvent interface for handling update events ([78019ee](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/78019eefb3582f769db0b65a68a8319e91cd987f))
* Add min and max properties for versitility ([a56c727](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/a56c727a4f65e9b0527c44945cd2f2cde55282a6))
* Add NextUp/Down math utility functions ([5e6fd11](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/5e6fd11f7d9da0aa8d19d335c114f92edf9ad1b9))
* Add NullCheck attribute and its property drawer ([67bc2be](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/67bc2be4421390ca015f83b0842135109e678298))
* Add Pascal Triangle singleton ([832e36c](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/832e36c948cf82325ebb0b8460a5b278fef5dc01))
* Add Quaternion Bezier curve interpolator ([09a80f2](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/09a80f239967abffed7a3c88c1d37c2f0bd44725))
* Add Resize and onDestroyValue to LRUCache ([d4b26f0](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/d4b26f0191e6004499976c1632d5db9303e7f86a))
* Add TweenBuilder class and update BaseTween class ([d483f12](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/d483f126486b2b825c7b6b45f38b5689d3125919))
* Add Unity inspector accessible version of EnumFuncMap ([c2e8529](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/c2e8529aae9805a11644a97faba9449bdb95809a))
* Add Vector2/3 interpolations ([6f72d95](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/6f72d95fe72d38c8bb08c684e7109ec78e19f53f))
* Adjust names of color tweens ([960d86a](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/960d86a723c401b96aa93e1aada99b96a31d30b5))
* Breaking down playback controls ([a7f0ab5](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/a7f0ab5c1eea2c5c3dc3722292c4ea957dc49c0f))
* Consolidate playback interface to essentials ([b21ccf3](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/b21ccf37a3956847e72c0d87cbee3b377bc070f7))
* Convert BaseTween to abstract, implement BasicTween ([752a154](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/752a1541d869a1c2c85e8116a604832282fd484f))
* Enforce progress range in ApplyProgress method ([ba7d8da](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/ba7d8da701e8adef58bbec7044e9095bd680a75e))
* Further consolidation and restructuring of playback controls ([5dab6b9](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/5dab6b93afe99718c3e43480d8d86ecd5963d6c0))
* Implement basic two point tween ([2094f74](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/2094f74f17b517684ba76ce4801f884735dfb874))
* Implement EnumToFuncMap class ([bfdf1c9](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/bfdf1c96d4a154f8e4056e1f2bd0d67a7ed96d13))
* Implement generic singleton abstraction ([32c8ac8](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/32c8ac880c7fdae7c546eeabb6d02f753b1c109f))
* Implement Luma Linear Color Bezier curve interpolation ([5d34b9b](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/5d34b9bc01b23c5370580e906adc6bbe2c1608e0))
* Implement quaternion tweens ([6c9c476](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/6c9c476a2011e11b97386c09fd1ca3e9d5f75418))
* Implement reference and value LRUCache ([61ce4e6](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/61ce4e6a5779ed1619f8920f266b9a050ce3a0ea))
* Implement RGB Color Bezier curve interpolation ([e5efae2](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/e5efae232838126bbf1f18b29de329a663e03621))
* Implement SmartCoroutine ([d6f1c1c](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/d6f1c1c40bf5fac4ed3c132272292dd866b343bb))
* Implement tweening functionality ([17799c4](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/17799c44ba873dcb9ced7dad5d74640403e4b4da))
* Implement Vector2 Bezier curve interpolation ([f48884c](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/f48884cedf30c29798af7a07bcfeef2a5e0d52fd))
* Implement Vector3 Bezier curve interpolation ([cdbda71](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/cdbda7188204586009787c5bbd1041dba45b2eba))
* Implement WeightedIndexPool layout and basics ([46935d2](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/46935d21f1542877c57800ce46db6d712aaafe2e))
* Mark EnumFuncMap and UnityEnumFuncMap as sealed ([5cc12b7](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/5cc12b77c28f1f712eebc74d6404676e3592d63a))
* Move func default from base to Unity version ([97aaf66](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/97aaf665f8e40801c93fe8a88acd4f58293a6da5))
* Rearrange Stop/Pause and integrate MBTweenPlayer ([ba1aa38](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/ba1aa3822e1ac344e6d17db711a8b23e07a8ea6b))
* Remove Show function from Tween interface ([5b42698](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/5b42698dda932e372c5976dfe96439957780c27d))
* Remove unneeded hideOnComplete parameter ([ba7ce86](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/ba7ce8669a748d807ba7a81a024823448eb37831))
* Removing unneeded interface ([77bc81e](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/77bc81e1a82c59ca25c1ab875a3bd5a568f1865e))
* Seal and refactor two point interpolators ([0e21369](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/0e2136989bc16b822f3e190ff09fd76799960326))
* Update interpolators with granular inheritance ([45d9d5c](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/45d9d5ce652235d60f4fd58ee9cfcf7f78a34c09))

# [1.4.0](https://github.com/alchemicalflux/com.alchemicalflux.utilities/compare/v1.3.1...v1.4.0) (2025-01-08)


### Bug Fixes

* Add granulated options for NullCheck menu items and add message when no errors occur ([b601f57](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/b601f57e950beae59c5f0efdae41b868871ff42a))
* Add NullCheck locating and validation functionality, menu items, and on load protocols ([0cbfe5d](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/0cbfe5d610ff7da1d2c28d90ae3df952336ece81))
* Add reflection utilities for finding fields on GameObjects with a specific attribute. ([a821505](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/a8215056caf4e315152bacb5ce5efe9e503a73d8))
* Adjust ApplyProgress return for start/end eval ([54e1f08](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/54e1f08b6168bb93d49b5df84fec1f9ede5322ff))
* Convert CachNode to class ([d4a09cc](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/d4a09ccbcd7f2d600ae9909c8fb70f0e83f2b7c9))
* Ensure value fields still display as violation when included in prefabs ([60352fc](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/60352fc198ce32332802649f676fad66ea37203e))
* Remove debug output from Singleton ([8e7e97d](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/8e7e97de1082b7111f2c314054d73f594421e5bc))
* Rename for better representation ([beb17c9](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/beb17c974bea33587a26c50e3ea3f1adcd90acf6))
* Uncomment accidentally removed Singleton calls ([de15bcb](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/de15bcb867329acfb8ff008aba6bd9331372df5a))


### Features

* Add initial WaitManager class ([3cff1ca](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/3cff1caead5c16fd062f19ab4129f2f21dc63825))
* Add NullCheck attribute and its property drawer ([67bc2be](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/67bc2be4421390ca015f83b0842135109e678298))
* Add Resize and onDestroyValue to LRUCache ([d4b26f0](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/d4b26f0191e6004499976c1632d5db9303e7f86a))
* Implement basic two point tween ([2094f74](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/2094f74f17b517684ba76ce4801f884735dfb874))
* Implement generic singleton abstraction ([32c8ac8](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/32c8ac880c7fdae7c546eeabb6d02f753b1c109f))
* Implement reference and value LRUCache ([61ce4e6](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/61ce4e6a5779ed1619f8920f266b9a050ce3a0ea))
* Implement SmartCoroutine ([d6f1c1c](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/d6f1c1c40bf5fac4ed3c132272292dd866b343bb))
* Implement tweening functionality ([17799c4](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/17799c44ba873dcb9ced7dad5d74640403e4b4da))

## [1.3.1](https://github.com/alchemicalflux/com.alchemicalflux.utilities/compare/v1.3.0...v1.3.1) (2023-11-04)


### Bug Fixes

* Update package template version and adjust to its changes ([9274454](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/9274454ca12033e266b9d75c5b5ca0b2b8c6035f))

# [1.3.0](https://github.com/alchemicalflux/com.alchemicalflux.utilities/compare/v1.2.0...v1.3.0) (2023-11-02)


### Bug Fixes

* Destroy temporary files from copy process ([6f6e91a](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/6f6e91a7ad7d845a738513e04fa3e7b373f68003))


### Features

* Add CopyDirectory and DeleteDirectory to IFileSystemService ([406c757](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/406c757787e31417a4780f7b76437c72906bf31c))
* Implement file copying for precommits and semantic release operations ([ef0888f](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/ef0888f433e7222ed4313eb70dc6d188899acf24))

# [1.2.0](https://github.com/alchemicalflux/com.alchemicalflux.utilities/compare/v1.1.0...v1.2.0) (2023-10-27)


### Bug Fixes

* Add unit test to Helper's Test namespace ([d5665f2](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/d5665f26fc2df97aad54e2a3be689ad62b0d4173))
* Match file name with the class being extended ([f77dff3](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/f77dff3a596b0a3f756033e823cf9e1c65e76e99))


### Features

* Add extension to merge to IDictionary into single enumerable collection ([e8f78c5](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/e8f78c5124a3bd7024346cd97648149d56d7a360))

# [1.1.0](https://github.com/alchemicalflux/com.alchemicalflux.utilities/compare/v1.0.0...v1.1.0) (2023-10-14)


### Bug Fixes

* Add initial editor window, logic, and UI scripts ([a1ee4a2](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/a1ee4a239f537c30f561a157aa29cb2fd8b90abf))
* Add missing headers and remove unused 'using's ([99bf4ea](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/99bf4ea12ff157ec023215d46c6c426e05d9eefa))
* Convert list view UI binding to callback through controller ([1c0fdbd](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/1c0fdbd6c5148758fbebecb674741710bbce30ad))
* Correct merge mishaps and package layout ([a91d2bf](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/a91d2bfe451d56173786bfd84c69800ba184267c))
* Decouple unit test from direct asset referencing using ScriptableObjects ([2141718](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/2141718950af135d85f0aa05aa52f8a1ca588296))
* Implement display of searched folders and selection options for git operations. ([5ef99bf](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/5ef99bf099197a75bebbecc9870cb4eb7a9804ca))
* Implement folder search and folder gather with UI ([3c51b16](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/3c51b16dfe2b59114084df98e24d87a699a550a0))
* Refactor GitOperationsEditor into the Shared folder ([cede019](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/cede019f4e2ea8ae06d104934fdbe70842da17fe))
* Refactor unit test ([d1ce802](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/d1ce8021dfa586ef172a700235130d4b8efff664))
* Remove non-functional dependency ([d84d480](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/d84d480498737d9bec8ab1b75f49401060414a0a))
* Separate runtime/editor sharable code into Shared folder ([5f07bed](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/5f07bedbd621f6f01fe07f09052e14172d8bf24c))


### Features

* Add GeneratePackageEditorWindow ([#1](https://github.com/alchemicalflux/com.alchemicalflux.utilities/issues/1)) ([009071a](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/009071a1457299d08a881565abf1da96c5094ae3))
* Add GitOperationsEditorWindow ([3ed70bb](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/3ed70bb77bfede178a5535a8aac483c180caf9dd))

# 1.0.0 (2023-06-22)


### Features

* Add GeneratePackageEditorWindow ([#1](https://github.com/alchemicalflux/com.alchemicalflux.utilities/issues/1)) ([#2](https://github.com/alchemicalflux/com.alchemicalflux.utilities/issues/2)) ([3cf50dc](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/3cf50dccbd088a1b46a90b50329a3de5e3cf1b11))
