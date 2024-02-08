
# Bijections Between Natural Numbers and Other Data Types

## What is a natural number bijection?

The bijections implemented here are one-to-one mappings between natural numbers (including zero) and other data types
including signed integers, non-negative rational numbers, tuples of fixed size, lists of natural numbers, and
sequences of symbols from an alphabet.

For example, the string "Hello, world!" can be represented by the list of Unicode codepoint values (or ASCII values)
[72, 101, 108, 108, 111, 44, 32, 119, 111, 114, 108, 100, 33]. This list can be mapped, using a bijective mapping,
to the value 521326923876532544869691613767074.

The important attributes of this mapping are:
- Every list corresponds to a different value. The values are guaranteed to be different if the lists are different. 
- Every value corresponds to a unique list, including the empty list which has a value of 0.

## Why Bijections?

The number obtained from a bijective mapping can be used for several purposes:

- Serializing and deserializing the value. Map a data value to a natural number, and then map the number to a unique 
  sequence of bytes (a sequence of symbols using an alphabet of size 256).

- Enumerating all possible values of a data structure, for testing or exploration. 
  Just map each natural number in sequence.

- Generating random values of a data structure for testing. Just generate a random natural number and map it.

## Supported bijections

|Natural|Integer|Pair of naturals|n-tuple of naturals|Ratio of naturals|List of naturals|Word (base 10 example)|
|:-:|:-:|:-:|:-:|:-:|:-:|:-:|
|0|0|(0, 0)|(0, 0, 0)|0|[]|[]|
|1|-1|(0, 1)|(0, 0, 1)|1|[0]|[0]|
|2|1|(1, 0)|(0, 1, 0)|1/2|[0, 0]|[1]|
|3|-2|(0, 2)|(1, 0, 0)|2|[1]|[2]|
|5|-3|(2, 0)|(0, 1, 1)|3/2|[3]|[4]|
|8|4|(2, 1)|(1, 1, 0)|1/4|[6]|[7]|
|13|-7|(3, 1)|(0, 2, 1)|5/3|[0, 0, 0]|[0, 2]|
|21|-11|(0, 6)|(0, 1, 3)|8/5|[2, 1]|[1, 0]|
|34|17|(6, 1)|(4, 0, 0)|5/9|[21]|[2, 3]|
|55|-28|(0, 10)|(5, 0, 0)|11/3|[42]|[4, 4]|
|89|-45|(11, 1)|(2, 0, 5)|17/12|[0, 0, 3]|[7, 8]|
|144|72|(8, 8)|(3, 3, 2)|4/19|[10, 0]|[0, 3, 3]|
|233|-117|(2, 19)|(3, 1, 6)|25/18|[154]|[1, 2, 2]|
|377|-189|(26, 0)|(3, 1, 8)|29/20|[1, 0, 2]|[2, 6, 6]|
|610|305|(15, 19)|(5, 4, 5)|31/55|[14, 7]|[4, 9, 9]|
|987|-494|(41, 2)|(3, 2, 12)|52/19|[622]|[8, 7, 6]|
|1597|-799|(1, 55)|(2, 8, 10)|67/37|[1, 3, 6]|[0, 4, 8, 6]|
|2584|1292|(28, 43)|(8, 15, 0)|31/107|[1218]|[1, 4, 7, 3]|
|4181|-2091|(86, 4)|(1, 14, 13)|139/86|[2815]|[3, 0, 7, 0]|
|6765|-3383|(95, 20)|(10, 10, 13)|282/163|[50, 18]|[5, 6, 5, 4]|

These can be combined, for example, to map naturals to pairs of rationals, by mapping to pairs and then mapping
each element of the pair to a rational.

|Natural|Pair of rationals|List of pairs|A-Z string|
|:-:|:-:|:-:|:-:|
|1|(0, 1)|[(0, 0)]|"A"
|2|(1, 0)|[(0, 0), (0, 0)]|"B"
|5|(1/2, 0)|[(0, 2)]|"E"
|12|(1/2, 1/2)|[(0, 4)]|"L"
|29|(1, 2/3)|[(1, 4)]|"AC"
|70|(1/3, 3)|[(2, 8)]|"BR"
|169|(1/5, 1)|[(12, 0)]|"FM"
|408|(1/2, 5/8)|[(1, 0), (0, 1), (1, 0)]|"OR"
|985|(10/3, 1/3)|[(25, 9)]|"AKW"
|2378|(1/6, 4/11)|[(22, 22)]|"CML"
|5741|(9/13, 4/11)|[(1, 8), (2, 0)]|"HLU"
|13860|(30/19, 0)|[(0, 0), (2, 3), (1, 0)]|"TMB"
|33461|(7/12, 5/23)|[(0, 3), (0, 2), (0, 1), (1, 0)]|"AWLY"
|80782|(31/19, 11/25)|[(1, 0), (3, 0), (0, 0), (0, 3)]|"DOLZ"
|195025|(7/5, 65/18)|[(3, 0), (3, 4), (3, 1)]|"KBLY"
|470832|(62/27, 12/17)|[(7, 15), (15, 5)]|"ZTLX"
|1136689|(46/17, 19/71)|[(200, 728)]|"BLQLU"
|2744210|(55/32, 54/37)|[(1, 1), (5, 1), (1, 4), (0, 1)]|"EZCLN"
|6625109|(79/62, 49/69)|[(1387, 694)]|"NLXKW"
|15994428|(20/73, 82/17)|[(4, 16), (3, 2), (6, 0)]|"AHYZJH"
|38613965|(101/22, 89/243)|[(2, 2), (2, 0), (10, 0), (0, 5)]|"CFLYFM"

Mappings can be recursively defined, for example to encode a term algebra to generate or encode
arbitrary expressions.

|Natural|Expression|
|:-:|:-:|
|1|$$y$$|
|2|$$\pi$$|
|5|$$\frac{x}{x}$$|
|12|$$\left(x-y\right)$$|
|29|$$\sqrt{\frac{x}{x}}$$|
|70|$$\frac{1}{y}$$|
|169|$$\sqrt{7}$$|
|408|$$82$$|
|985|$$\frac{\left(x+x\right)}{3}$$|
|2378|$$476$$|
|5741|$$\left(\sqrt{1}+6\right)$$|
|13860|$$\frac{\frac{1}{y}}{1}$$|
|33461|$$\left(\left(x+\pi\right)+\sqrt{4}\right)$$|
|80782|$$\left(\frac{\pi}{y}-\sqrt{\left(y+y\right)}\right)$$|
|195025|$$\frac{45}{\frac{x}{\sqrt{x}}}$$|
|470832|$$\left(\sqrt{\frac{y}{\pi}}-\sqrt{\frac{\pi}{y}}\right)$$|
|1136689|$$\sqrt{\left(\frac{y}{x}-\left(y+\sqrt{y}\right)\right)}$$|
|2744210|$$\frac{43}{\sqrt{\left(\sqrt{x}+1\right)}}$$|
|6625109|$$\sqrt{\left(\frac{2}{1}+\left(1-2\right)\right)}$$|
|15994428|$$3198886$$|
|38613965|$$\frac{\left(\frac{y}{y}-\sqrt{x}\right)}{\left(5-y\right)}$$|