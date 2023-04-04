<template>
    <div @click="setActiveKeep(keep.id)" class="container" data-bs-toggle="modal" data-bs-target="#keepDetails">
        <div class="card rounded elevation-1">
            <img class="img-fluid rounded" :src="keep.img" :alt="keep.name">
            <div class="card-img-overlay">
                <div class="row">
                    <div class="col-12" v-if="account.id == keep?.creator.id">
                        <i @click="deleteKeep(keep.id)"
                            class="d-flex justify-content-end selectable fs-3 text-danger mdi mdi-alpha-x-circle"></i>
                    </div>
                </div>
                <div class="row d-flex align-content-end">
                    <div class="col-6">
                        <h4 class="p-2 text-light text-style mt-5">{{ keep?.name }}</h4>
                    </div>
                    <div class="col-6">
                        <div class="d-flex justify-content-end ">
                            <router-link class="selectable"
                                :to="{ name: 'Profile', params: { profileId: keep.creatorId } }">
                                <img class="profile-img" :src="keep?.creator?.picture" alt="" :title="keep?.creator?.name">
                            </router-link>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- <div class="card bg-white rounded elevation-1">
            <img class="img-fluid rounded-top" :src="keep.img" alt="">
            <div class="card-img-overlay d-flex row align-content-between">
                <div class="col-12 d-flex justify-content-end" v-if="account.id == keep?.creator.id">
                    <i @click="deleteKeep(keep.id)" class=" selectable fs-3 text-danger mdi mdi-alpha-x-circle"></i>
                </div>
                <div class="col-12 d-flex justify-content-between">
                   
                    
                </div>
            </div>
        </div> -->

    </div>
</template>


<script>
import { AppState } from '../AppState.js';
import { Keep } from '../models/Keep.js';
import { computed } from 'vue';
import { keepsService } from '../services/KeepsService.js';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';

export default {
    props: { keep: { type: Keep, required: true } },
    setup(props) {
        return {
            keeps: computed(() => AppState.keep),
            account: computed(() => AppState.account),

            setActiveKeep(keepId) {
                keepsService.setActiveKeep(keepId)
                if (this.keeps.creatorId != this.account.id)
                    this.keeps.views++
            },

            // async getKeepById(keepId) {
            //     try {
            //         const keepId 
            //     } catch (error) {
            //         logger.error(error)
            //         Pop.error(error.message)
            //     }
            // },

            async deleteKeep(keepId) {
                try {
                    if (await Pop.confirm('Are you sure you want to delete this keep?')) {
                        await keepsService.deleteKeep(keepId)
                    }
                } catch (error) {
                    logger.error(error)
                    Pop.error(error.message)
                }
            }
        }
    }
}
</script>


<style lang="scss" scoped>
.profile-img {
    height: 60px;
    width: 60px;
    border-radius: 50%;
}

.text-style {
    text-shadow: 0 0 4px rgb(0, 0, 0);
}

// .delete-style {
//     transform: translateY(-5vh);
//     transform: translateX(-2vh);
// }
</style>