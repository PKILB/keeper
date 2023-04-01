<template>
  <div class="container-fluid py-2 bg-grey">
    <div class="row">
      <div class="col-md-10 m-auto">
        <section class="bricks">
          <div v-for="k in keeps" class="pt-2">
            <KeepCard :keep="k" />
          </div>
        </section>
      </div>
    </div>
  </div>

  <KeepModalComponent id="keepDetails">
    <KeepDetails />
  </KeepModalComponent>
</template>

<script>
import { onMounted, computed } from 'vue';
import { AppState } from '../AppState.js';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';
import { keepsService } from '../services/KeepsService.js';
import KeepCard from '../components/KeepCard.vue';
// import KeepDetails from '../components/KeepDetails.vue';

export default {
  setup() {
    onMounted(() => {
      getAllKeeps();
    });
    async function getAllKeeps() {
      try {
        await keepsService.getAllKeeps();
      }
      catch (error) {
        logger.error(error);
        Pop.error(error);
      }
    }
    return {
      keeps: computed(() => AppState.keeps)
    };
  },
  components: { KeepCard }
}
</script>

<style scoped lang="scss">
$gap: .5em;

.bricks {
  columns: 300px;
  column-gap: $gap;

  &>div {
    margin-top: $gap;
    display: inline-block;
  }
}
</style>
