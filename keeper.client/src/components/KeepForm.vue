<template>
    <div class="modal-content">
        <div class="modal-header">
            <h1 class="modal-title fs-5" id="createKeepModal">Add Your Keep</h1>
            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <form @submit.prevent="createKeep()">
            <div class="modal-body">
                <div class="mb-3">
                    <label for="name" class="form-label">Name...</label>
                    <input v-model="editable.name" required type="text" class="form-control" id="name"
                        aria-describedby="emailHelp">
                </div>
                <div class="mb-3">
                    <label for="img" class="form-label">Image URL...</label>
                    <input v-model="editable.img" required type="url" class="form-control" id="img"
                        aria-describedby="emailHelp">
                </div>
                <div class="mb-3">
                    <label for="description" class="form-label">Keep Description...</label>
                    <input v-model="editable.description" required type="textarea" class="form-control" id="description"
                        aria-describedby="emailHelp">
                </div>
            </div>
            <div class="modal-footer">
                <button type="submit" class="btn btn-primary" data-bs-dismiss="modal">
                    Create
                </button>
            </div>
        </form>
    </div>
</template>


<script>
import { ref } from 'vue';
import { Keep } from '../models/Keep.js';
import { keepsService } from '../services/KeepsService.js';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';

export default {
    props: {
        keep: { type: Keep, required: true }
    },

    setup() {

        const editable = ref({})

        return {
            editable,
            async createKeep() {
                try {
                    const formData = editable.value
                    const keep = await keepsService.createKeep(formData)
                } catch (error) {
                    logger.error(error)
                    Pop.error(error.message)
                }
            }
        }
    }
}
</script>


<style lang="scss" scoped></style>