SndBuf buffy => DelayL d1 => PitShift p => Pan2 pan => Gain g1 =>  dac;
buffy => DelayL d2 => PitShift p3 => Pan2 pan2 => Gain g3 =>  dac;
buffy => Gain g => dac;

buffy => Chorus c => Gain g4 => dac;
buffy => DelayL d6 => Chorus c2 => g4 => dac;
buffy => DelayL d3 => PitShift p6 => Gain g5 => dac;

//deeper voices
buffy => DelayL d4 => PitShift p5 => Pan2 pan3 => Gain g6 =>  dac;
buffy => DelayL d5 => p5 => Pan2 pan4 => Gain g7 =>  dac;

me.dir() + "final.wav" => buffy.read;
0 => buffy.pos;

1.0 => p.mix;
0.8 => p.shift;
1.0 => p3.mix;
1.25 => p3.shift;
1.0 => p5.mix;
0.5 => p5.shift;
1.0 => p6.mix;
2.0 => p6.shift;
0.6 => g1.gain;
0.6 => g3.gain;
0.45 => g.gain;
0.0 => g4.gain;
0.0 => g5.gain;
0.0 => g6.gain;
0.0 => g7.gain;

-0.8 => pan.pan;
0.95 => pan2.pan;
-1 => pan3.pan;
1 => pan4.pan;

80::ms => d1.max => d1.delay;
75::ms => d2.max => d2.delay;
100::ms => d4.max => d4.delay;
90::ms => d5.max => d5.delay;
1766::ms => d3.max => d3.delay;
120::ms => d5.max => d5.delay;
2::second => now;
0.35 => g6.gain;
0.35 => g7.gain;
32::second => now;

<<<"welcome to 30s">>>;
0.21 => g4.gain;
31::second => now;
100::ms => now;

0.3 => c2.modFreq;
0.6 => c2.modDepth;
<<<"welcome to 1m4s">>>;
0.6 => g5.gain;
1.5::second => now;
for ( 0 => int foo; foo < 9 ; foo++ ) {
    0.6 - (0.6 * (foo / 8)) => g5.gain;
    0.2::second => now; 
}
/**
0.1::second => now;
<<<now>>>;;;
0.0 => g5.gain;
4866::ms => now;
0.6 => g5.gain;
2.2::second => now;
0.0 => g5.gain;
*/

while (true) {
    200::ms => now;
}