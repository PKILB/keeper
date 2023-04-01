<template>
    <div @click="setActiveKeep()" class="container selectable" data-bs-toggle="modal" data-bs-target="#keepDetails">
        <div class="card bg-white rounded elevation-1">
            <img class="img-fluid rounded-top" :src="keep.img" alt="">
            <div class="card-img-overlay row d-flex align-content-end">
                <h4 class="p-2 text-light text-style col-6 mt-5">{{ keep?.name }}</h4>
                <div class=" p-3 col-6 d-flex justify-content-end mt-5 ">
                    <img class="profile-img" :src="keep?.creator?.picture" alt="" :title="keep?.creator?.name">
                </div>
            </div>
        </div>

    </div>
</template>


<script>
import { AppState } from '../AppState.js';
import { Keep } from '../models/Keep.js';
import { computed } from 'vue';
import { keepsService } from '../services/KeepsService.js';

export default {
    props: { keep: { type: Keep, required: true } },
    setup(props) {
        return {
            keeps: computed(() => AppState.keep),

            setActiveKeep() {
                keepsService.setActiveKeep(props.keep)
            }
        }
    }
}
</script>


<style lang="scss" scoped>
.profile-img {
    height: 10vh;
    width: 10vh;
    border-radius: 50%;
}

.text-style {
    text-shadow: 0 0 4px rgb(0, 0, 0);
}
</style>