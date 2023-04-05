<template>
    <div class="modal fade" id="modalId" tabindex="-1" aria-labelledby="modalTitleId" aria-hidden="true">
        <div class="modal-dialog modal-dialog-scrollable modal-dialog-centered modal-lg" role="document">
            <div class="modal-content">
                <slot></slot>
            </div>
        </div>

    </div>
</template>


<script>
export default {
    setup() {
        return {}
    }
}
</script>


<style lang="scss" scoped>
.scroll-y {
    overflow-y: scroll;
    height: calc(100vh - 122px);
}
</style>