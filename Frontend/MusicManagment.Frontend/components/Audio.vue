<template>
    <div class="card m-3">
        <div class="card-content">
            <div class="media"></div>
                <div class="media-content">
                    <p class="title is-4">{{ name }}</p>
                    <p class="subtitle is-6" :href="artist.link">{{ artist.name }}</p>
                </div>
            </div>
            <div v-if="isPlaying">
                <audio id="player" ontimeupdate="">
                    <source :src="source" type="audio/mp3">
                </audio>
                <progress class="progress is-primary" value="15" max="100">15%</progress>
                <div class="content has-text-centered">
                    <PhSkipBack :size="38" :color="colors.skipBack"/>
                    <PhPlay :size="48" :color="colors.play" @click="playPause()"/>
                    <PhSkipForward :size="38" :color="colors.skipForward"/>
                    <input type="range" min="0" max="1" step="0.1" v-model="controll.volume">
                </div>
            </div>
        </div>
    </div>
</template>
<script>
  import { PhSkipBack, PhSkipForward, PhPlay } from "phosphor-vue";
  export default {
    name: "Audio",
    components: {
      PhSkipBack,
      PhSkipForward,
      PhPlay
    },
    data: () => {
        return {
            isPlaying: true,
            name: 'In the city',
            link: '',
            source: '',
            controll: {
                volume: 0
            },
            artist: {
                name: 'XXX_Burn_it_all_down_XXX',
                link: ''
            },
            colors: {
                skipBack: '#000000',
                play: '#000000',
                skipForward: '#000000'
            }
        }
    },
    methods: {
        playPause() {
            let player = document.getElementById('player');
            if (player.paused) {
                player.play()
                console.log('play');
            } else {
                player.pause()
                console.log('pause');
            }
        },
        updateTime() {
            let player = document.getElementById('player');
        }
    }
    
  };
</script>