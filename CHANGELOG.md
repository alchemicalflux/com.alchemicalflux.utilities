# [1.4.0](https://github.com/alchemicalflux/com.alchemicalflux.utilities/compare/v1.3.1...v1.4.0) (2025-01-03)


### Bug Fixes

* Add granulated options for NullCheck menu items and add message when no errors occur ([b601f57](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/b601f57e950beae59c5f0efdae41b868871ff42a))
* Add NullCheck locating and validation functionality, menu items, and on load protocols ([0cbfe5d](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/0cbfe5d610ff7da1d2c28d90ae3df952336ece81))
* Add reflection utilities for finding fields on GameObjects with a specific attribute. ([a821505](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/a8215056caf4e315152bacb5ce5efe9e503a73d8))
* Adjust ApplyProgress return for start/end eval ([54e1f08](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/54e1f08b6168bb93d49b5df84fec1f9ede5322ff))
* Ensure value fields still display as violation when included in prefabs ([60352fc](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/60352fc198ce32332802649f676fad66ea37203e))
* Remove debug output from Singleton ([8e7e97d](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/8e7e97de1082b7111f2c314054d73f594421e5bc))
* Rename for better representation ([beb17c9](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/beb17c974bea33587a26c50e3ea3f1adcd90acf6))
* Uncomment accidentally removed Singleton calls ([de15bcb](https://github.com/alchemicalflux/com.alchemicalflux.utilities/commit/de15bcb867329acfb8ff008aba6bd9331372df5a))


### Features

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
